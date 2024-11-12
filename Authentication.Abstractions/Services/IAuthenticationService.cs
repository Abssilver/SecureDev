using Authentication.Abstractions.Dto;
using Validation.Abstractions;

namespace Authentication.Abstractions.Services;

public interface IAuthenticationService
{
    Task<IOperationResult<AuthResponseDto>> Authenticate(AuthRequestDto authRequest);
    Task<IOperationResult<int>> CreateUser(CreateAccountDto dto);
    Task<IOperationResult<SessionInfoDto>> GetSessionInfo(string sessionToken);
}