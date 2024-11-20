namespace Validation.Abstractions;

public interface IBusinessLogicOperationFailureFactory
{
    IOperationFailure CreateCardsGettingFailure();
    IOperationFailure CreateClientCreationFailure();
}