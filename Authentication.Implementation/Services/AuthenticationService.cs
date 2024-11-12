using System.Collections.Concurrent;
using Authentication.Abstractions.Dto;
using Authentication.Abstractions.Services;
using AutoMapper;
using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Validation.Abstractions;
using Validation.Implementation;

namespace Authentication.Implementation.Services;

public class AuthenticationService: IAuthenticationService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMemoryCache _memoryCache;
    private readonly IAuthOperationFailureFactory _failureFactory;
    private readonly IServiceProvider _serviceProvider;

    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new ();
    
    public AuthenticationService(
        IServiceScopeFactory scopeFactory,
        IMemoryCache memoryCache,
        IAuthOperationFailureFactory failureFactory, 
        IServiceProvider serviceProvider
        )
    {
        _scopeFactory = scopeFactory;
        _memoryCache = memoryCache;
        _failureFactory = failureFactory;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<IOperationResult<AuthResponseDto>> Authenticate(AuthRequestDto authRequest)
    {
        if (string.IsNullOrWhiteSpace(authRequest.Login))
            return new OperationResult<AuthResponseDto>(new AuthResponseDto
            {
                Status = AuthenticationStatus.InvalidUserName,
                SessionInfo = SessionInfoDto.Empty,
            }, _failureFactory.CreateAuthenticationInvalidLoginFailure());

        await using var scope = _scopeFactory.CreateAsyncScope();
        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();

        var account = await accountRepository.GetByEmail(authRequest.Login);
        if (account == AccountEntity.Empty)
        {
            return new OperationResult<AuthResponseDto>(new AuthResponseDto
            {
                Status = AuthenticationStatus.UserNotFound,
                SessionInfo = SessionInfoDto.Empty,
            }, _failureFactory.CreateAuthenticationNotFoundUserFailure());
        }

        var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordSecurityService>();
        if (!passwordService.VerifyPassword(authRequest.Password, account.PasswordSalt, account.PasswordHash))
        {
            return new OperationResult<AuthResponseDto>(new AuthResponseDto
            {
                Status = AuthenticationStatus.InvalidPassword,
                SessionInfo = SessionInfoDto.Empty,
            }, _failureFactory.CreateAuthenticationInvalidPasswordFailure());
        }

        var sessionRepository = scope.ServiceProvider.GetRequiredService<IAccountSessionRepository>();
        var tokenProvider = scope.ServiceProvider.GetRequiredService<ITokenProvider>();
        var mapper = _serviceProvider.GetRequiredService<IMapper>();
        var accountDto = mapper.Map<AccountDto>(account);
        var entity = new AccountSessionEntity
        {
            SessionToken = tokenProvider.GenerateAccessToken(accountDto),
            AccountId = account.Id,
            TimeCreated = DateTime.UtcNow,
            TimeLastRequest = DateTime.UtcNow,
            IsClosed = false,
        };
        await sessionRepository.CreateAsync(entity);
        var sessionInfoDto = new SessionInfoDto
        {
            Id = entity.Id,
            Token = entity.SessionToken,
            Account = accountDto,
        };
        await CacheSessionInfo(sessionInfoDto);
        return new OperationResult<AuthResponseDto>(new AuthResponseDto
        {
            Status = AuthenticationStatus.Success,
            SessionInfo = sessionInfoDto,
        }, ArraySegment<IOperationFailure>.Empty);
    }

    public async Task<IOperationResult<int>> CreateUser(CreateAccountDto dto)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();

        var user = await accountRepository.GetByEmail(dto.Email);
        if (user != AccountEntity.Empty)
            return new OperationResult<int>(-1, _failureFactory.CreateUserIsAlreadyExistFailure());
       
        var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordSecurityService>();
        var salt = passwordService.CreateSalt();
        var entity = new AccountEntity
        {
            Email = dto.Email,
            PasswordSalt = salt,
            PasswordHash = passwordService.GetPasswordHash(dto.Password, salt),
            FirstName = dto.FirstName,
            Surname = dto.Surname,
            Patronymic = dto.Patronymic,
        };
        var userId = await accountRepository.CreateAsync(entity);
        return new OperationResult<int>(userId, ArraySegment<IOperationFailure>.Empty);
    }

    public async Task<IOperationResult<SessionInfoDto>> GetSessionInfo(string sessionToken)
    {
        SessionInfoDto sessionInfo;
        if (_memoryCache.TryGetValue(sessionToken, out var cached))
        {
            if (cached != null)
            {
                sessionInfo = (SessionInfoDto)cached;
                return new OperationResult<SessionInfoDto>(sessionInfo, ArraySegment<IOperationFailure>.Empty);
            }
        }
        
        await using var scope = _scopeFactory.CreateAsyncScope();
        var sessionRepository = scope.ServiceProvider.GetRequiredService<IAccountSessionRepository>();
        var session = await sessionRepository.GetByTokenAsync(sessionToken);
        if (session == AccountSessionEntity.Empty)
            return new OperationResult<SessionInfoDto>(SessionInfoDto.Empty,
                _failureFactory.CreateInvalidSessionTokenFailure());

        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
        var account = await accountRepository.GetByIdAsync(session.AccountId);
        var mapper = _serviceProvider.GetRequiredService<IMapper>();
        var accountDto = mapper.Map<AccountDto>(account);
        sessionInfo = new SessionInfoDto
        {
            Id = session.Id,
            Token = session.SessionToken,
            Account = accountDto,
        };
        await CacheSessionInfo(sessionInfo);
        return new OperationResult<SessionInfoDto>(sessionInfo, ArraySegment<IOperationFailure>.Empty);
    }

    private async Task CacheSessionInfo(SessionInfoDto session)
    {
        var key = session.Token;
        if (!_memoryCache.TryGetValue(key, out _))
        {
            var slimLock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
            await slimLock.WaitAsync();
            try
            {
                if (!_memoryCache.TryGetValue(session.Token, out _))
                {
                    _memoryCache.Set(key, session);
                }
            }
            finally
            {
                slimLock.Release();
            }
        }
    }
}