namespace Authentication.Abstractions.Services;

public interface IPasswordSecurityService
{
    string CreateSalt();
    string GetPasswordHash(string password, string passwordSalt);
    bool VerifyPassword(string passwordToVerify, string passwordSalt, string passwordHash);
}