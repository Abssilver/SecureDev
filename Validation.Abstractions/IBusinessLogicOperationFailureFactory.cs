namespace Validation.Abstractions;

public interface IBusinessLogicOperationFailureFactory
{
    IOperationFailure CreateCardCreationFailure();
    IOperationFailure CreateCardsGettingFailure();
    IOperationFailure CreateClientCreationFailure();
}