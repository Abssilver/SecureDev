using Validation.Abstractions;
using Validation.Implementation;

namespace SecureDev.Controllers.Card;

public class CreateCardResponse: OperationResult<string>
{
    public CreateCardResponse(string result, IReadOnlyList<IOperationFailure> failures) : base(result, failures)
    {
    }

    public CreateCardResponse(string result, IOperationFailure failure) : base(result, failure)
    {
    }
}