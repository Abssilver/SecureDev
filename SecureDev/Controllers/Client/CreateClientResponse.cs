using Validation.Abstractions;
using Validation.Implementation;

namespace SecureDev.Controllers.Client;

public class CreateClientResponse: OperationResult<int>
{
    public CreateClientResponse(int result, IReadOnlyList<IOperationFailure> failures) : base(result, failures)
    {
    }

    public CreateClientResponse(int result, IOperationFailure failure) : base(result, failure)
    {
    }
}