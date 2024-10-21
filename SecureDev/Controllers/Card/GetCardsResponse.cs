using BusinessLogic.Abstractions.Dto;
using Validation.Abstractions;
using Validation.Implementation;

namespace SecureDev.Controllers.Card;

public class GetCardsResponse : OperationResult<IEnumerable<CardDto>>
{
    public GetCardsResponse(IEnumerable<CardDto> result, IReadOnlyList<IOperationFailure> failures) : base(result, failures)
    {
    }

    public GetCardsResponse(IEnumerable<CardDto> result, IOperationFailure failure) : base(result, failure)
    {
    }
}