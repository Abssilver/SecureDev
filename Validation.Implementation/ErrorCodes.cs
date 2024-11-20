namespace Validation.Implementation;

public static class ErrorCodes
{
    public const string CardCreationFailure = "BL-1012";
    public const string CardsGettingFailure = "BL-1013";
    public const string ClientCreationFailure = "BL-912";

    public const string AuthenticationInvalidLoginFailure = "AUTH-1";
    public const string AuthenticationNotFoundUserFailure = "AUTH-2";
    public const string AuthenticationInvalidPasswordFailure = "AUTH-3";
    public const string CreateUserFailure = "AUTH-4";
    public const string InvalidSessionTokenFailure = "AUTH-5";
    public const string AuthenticationWeakPasswordFailure = "AUTH-6";
    public const string CreateUserEmailFailure = "AUTH-7";
    public const string CreateUserFirstNameFailure = "AUTH-8";
    public const string CreateUserSurnameFailure = "AUTH-9";
    public const string CreateUserPatronymicFailure = "AUTH-10";
    public const string CreateUserWeakPasswordFailure = "AUTH-11";

    public static readonly Dictionary<string, string> ErrorCodeDescription = new()
    {
        { CardCreationFailure, "Card creation failure" },
        { CardsGettingFailure, "Get cards failure" },
        { ClientCreationFailure, "Client creation failure" },
        
        { AuthenticationInvalidLoginFailure, "Provided login has invalid values" },
        { AuthenticationNotFoundUserFailure, "User is not exist" },
        { AuthenticationInvalidPasswordFailure, "Provided password is invalid" },
        { CreateUserFailure, "User with the same username is already exist" },
        { InvalidSessionTokenFailure, "Provided session token is invalid" },
        { AuthenticationWeakPasswordFailure, "Password size must be between 5 and 20. It must contains numbers, lowercase and uppercase letters" },
        { CreateUserEmailFailure, "Provided email data is invalid" },
        { CreateUserFirstNameFailure, "Provided first name data is invalid" },
        { CreateUserFirstNameFailure, "Provided surname data is invalid" },
        { CreateUserPatronymicFailure, "Patronymic data is invalid" },
        { CreateUserWeakPasswordFailure, "Password is weak. Use numbers, lowercase and uppercase letters and length in range between 5 and 20" },
    };
}