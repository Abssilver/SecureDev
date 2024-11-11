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

    public static readonly Dictionary<string, string> ErrorCodeDescription = new()
    {
        { CardCreationFailure, "Card creation failure" },
        { CardsGettingFailure, "Get cards failure" },
        { ClientCreationFailure, "Client creation failure" },
        
        { AuthenticationInvalidLoginFailure, "Provided login has invalid values" },
        { AuthenticationNotFoundUserFailure, "User is not exist" },
        { AuthenticationInvalidPasswordFailure, "Provided password is invalid" },
        { CreateUserFailure, "User with the same username is already exist" },
        { InvalidSessionTokenFailure, "Provided session token is expired" }
    };
}