using Authentication.Abstractions.Dto;
using Validation.Abstractions;
using Validation.Implementation;

namespace Authentication.Implementation.Controllers;

public class AuthResponse: OperationResult<AuthResponseDto>
{
    public AuthResponse(AuthResponseDto result, IReadOnlyList<IOperationFailure> failures) : base(result, failures)
    {
    }

    public AuthResponse(AuthResponseDto result, IOperationFailure failure) : base(result, failure)
    {
    }
}