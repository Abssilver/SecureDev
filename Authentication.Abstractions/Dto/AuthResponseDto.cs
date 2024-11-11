namespace Authentication.Abstractions.Dto;

public class AuthResponseDto
{
    public AuthenticationStatus Status { get; set; }
    public SessionInfoDto SessionInfo { get; set; }
}