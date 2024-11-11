namespace Authentication.Abstractions.Configs;

public class JwtConfig
{
    public string JwtSecretKey { get; set; }
    public int AccessExpirationTimeInSeconds { get; set; }
}