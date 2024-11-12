using Authentication.Abstractions.Dto;
using Validation.Abstractions;
using Validation.Implementation;

namespace Authentication.Implementation.Controllers;

public class GetSessionInfoResponse: OperationResult<SessionInfoDto>
{
    public GetSessionInfoResponse(SessionInfoDto result, IReadOnlyList<IOperationFailure> failures) : base(result, failures)
    {
    }

    public GetSessionInfoResponse(SessionInfoDto result, IOperationFailure failure) : base(result, failure)
    {
    }
}