using BusinessLogic.Abstractions.Dto;
using Validation.Abstractions;

namespace Validation.Implementation.BusinessLogic;

public class BusinessLogicOperationFailureFactory: IBusinessLogicOperationFailureFactory
{
    public IOperationFailure CreateCardsGettingFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(ClientDto.Id),
            Description = ErrorCodes.ErrorCodeDescription[ErrorCodes.CardsGettingFailure],
            ErrorCode = ErrorCodes.CardsGettingFailure,
        };
    }

    public IOperationFailure CreateClientCreationFailure()
    {
        return new OperationFailure
        {
            PropertyName = nameof(ClientDto.Id),
            Description = ErrorCodes.ErrorCodeDescription[ErrorCodes.ClientCreationFailure],
            ErrorCode = ErrorCodes.ClientCreationFailure,
        };
    }
}