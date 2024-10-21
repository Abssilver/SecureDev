using BusinessLogic.Abstractions.Dto;
using Validation.Abstractions;
using Validation.Implementation.ErrorCodes;

namespace Validation.Implementation.BusinessLogic;

public class BusinessLogicOperationFailureFactory: IBusinessLogicOperationFailureFactory
{
    public IOperationFailure CreateCardCreationFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(CardDto.CardNumber),
            Description = BusinessLogicErrorCodes.CardCreationFailureDescription,
            ErrorCode = BusinessLogicErrorCodes.CardCreationFailure,
        };
    }

    public IOperationFailure CreateCardsGettingFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(ClientDto.Id),
            Description = BusinessLogicErrorCodes.CardsGettingFailureDescription,
            ErrorCode = BusinessLogicErrorCodes.CardsGettingFailure,
        };
    }

    public IOperationFailure CreateClientCreationFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(ClientDto.Id),
            Description = BusinessLogicErrorCodes.ClientCreationFailureDescription,
            ErrorCode = BusinessLogicErrorCodes.ClientCreationFailure,
        };
    }
}