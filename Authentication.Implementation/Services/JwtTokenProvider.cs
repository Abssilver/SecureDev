using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Abstractions.Configs;
using Authentication.Abstractions.Dto;
using Authentication.Abstractions.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Implementation.Services;

public class JwtTokenProvider: ITokenProvider
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenProvider(IOptions<JwtConfig> jwtOptions)
    {
        _jwtConfig = jwtOptions.Value;
    }

    public string GenerateAccessToken(AccountDto account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.JwtSecretKey);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
            }),
            Expires = DateTime.UtcNow.AddSeconds(_jwtConfig.AccessExpirationTimeInSeconds),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        
        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
}