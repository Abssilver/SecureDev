namespace Validation.Implementation;

public static class ErrorCodes
{
    public const string CardsGettingFailure = "BL-1";
    public const string CardCreationCardNumberFailure = "BL-2";
    public const string CardCreationExpDateFailure = "BL-3";
    public const string ClientCreationFirstNameFailure = "BL-4";
    public const string ClientCreationSurnameFailure = "BL-5";

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
        { CardsGettingFailure, "Get cards failure" },
        { CardCreationCardNumberFailure, "Card number data is invalid" },
        { CardCreationExpDateFailure, "Card expiration date has invalid value" },
        { ClientCreationFirstNameFailure, "Invalid client first name" },
        { ClientCreationSurnameFailure, "Invalid client surname" },
        
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