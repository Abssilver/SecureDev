namespace Validation.Abstractions;

public interface IAuthOperationFailureFactory
{
    IOperationFailure CreateAuthenticationInvalidLoginFailure();
    IOperationFailure CreateAuthenticationNotFoundUserFailure();
    IOperationFailure CreateAuthenticationInvalidPasswordFailure();
    IOperationFailure CreateUserIsAlreadyExistFailure();
    IOperationFailure CreateInvalidSessionTokenFailure();
}