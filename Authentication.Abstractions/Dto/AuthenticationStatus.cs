namespace Authentication.Abstractions.Dto;

public enum AuthenticationStatus
{
    Success = 0,
    UserNotFound = 1,
    InvalidPassword = 2,
    InvalidUserData = 3,
}