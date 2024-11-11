using Authentication.Abstractions.Dto;

namespace Authentication.Abstractions.Services;

public interface ITokenProvider
{
    string GenerateAccessToken(AccountDto account);
}