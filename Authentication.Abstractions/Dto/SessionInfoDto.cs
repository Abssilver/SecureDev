namespace Authentication.Abstractions.Dto;

public class SessionInfoDto
{
    public static SessionInfoDto Empty { get; }

    static SessionInfoDto()
    {
        Empty = new SessionInfoDto
        {
            Id = -1,
            Token = string.Empty,
            Account = new AccountDto()
        };
    }
    public int Id { get; set; }
    public string Token { get; set; }
    public AccountDto Account { get; set; }
}