using Authentication.Abstractions.Dto;
using Validation.Abstractions;

namespace Validation.Implementation.Authentication;

public class AuthOperationFailureFactory: IAuthOperationFailureFactory
{
    public IOperationFailure CreateAuthenticationNotFoundUserFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(AuthRequestDto.Login),
            Description = ErrorCodes.ErrorCodeDescription[ErrorCodes.AuthenticationNotFoundUserFailure],
            ErrorCode = ErrorCodes.AuthenticationNotFoundUserFailure,
        };
    }

    public IOperationFailure CreateAuthenticationInvalidPasswordFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(AuthRequestDto.Password),
            Description = ErrorCodes.ErrorCodeDescription[ErrorCodes.AuthenticationNotFoundUserFailure],
            ErrorCode = ErrorCodes.AuthenticationNotFoundUserFailure,
        };
    }

    public IOperationFailure CreateUserIsAlreadyExistFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(AccountDto.Email),
            Description = ErrorCodes.ErrorCodeDescription[ErrorCodes.CreateUserFailure],
            ErrorCode = ErrorCodes.CreateUserFailure,
        };
    }

    public IOperationFailure CreateInvalidSessionTokenFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(SessionInfoDto.Token),
            Description = ErrorCodes.ErrorCodeDescription[ErrorCodes.InvalidSessionTokenFailure],
            ErrorCode = ErrorCodes.InvalidSessionTokenFailure,
        };
    }
}