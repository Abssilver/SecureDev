using System.Security.Cryptography;
using System.Text;
using Authentication.Abstractions.Configs;
using Authentication.Abstractions.Services;
using Microsoft.Extensions.Options;

namespace Authentication.Implementation.Services;

public class PasswordSecurityService: IPasswordSecurityService
{
    private readonly SecurityConfig _securityConfig;

    public PasswordSecurityService(IOptions<SecurityConfig> securityOptions)
    {
        _securityConfig = securityOptions.Value;
    }

    public string CreateSalt()
    {
        var buffer = new byte[16];
        RandomNumberGenerator.Create().GetBytes(buffer); 
        var salt = Convert.ToBase64String(buffer);
        return salt;
    }

    public string GetPasswordHash(string password, string passwordSalt)
    {
        var toHash = $"{password}~{passwordSalt}~{_securityConfig.PasswordSecretKey}";
        var buffer = Encoding.UTF8.GetBytes(toHash);
        var passwordHash = SHA512.Create().ComputeHash(buffer);
        
        return Convert.ToBase64String(passwordHash);
    }

    public bool VerifyPassword(string passwordToVerify, string passwordSalt, string passwordHash)
    {
        return GetPasswordHash(passwordToVerify, passwordSalt) == passwordHash;
    }
}