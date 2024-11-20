namespace Validation.Abstractions;

public interface IAuthOperationFailureFactory
{
    IOperationFailure CreateAuthenticationNotFoundUserFailure();
    IOperationFailure CreateAuthenticationInvalidPasswordFailure();
    IOperationFailure CreateUserIsAlreadyExistFailure();
    IOperationFailure CreateInvalidSessionTokenFailure();
}