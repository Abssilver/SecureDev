using Authentication.Abstractions.Dto;
using FluentValidation;

namespace Validation.Implementation.Authentication;

public class AuthRequestValidation: ValidationWrapper<AuthRequestDto>
{
    public AuthRequestValidation()
    {
        RuleFor(x => x.Login)
            .NotNull()
            .NotEmpty()
            .NotEqual("undefined")
            .NotEqual("null")
            .MinimumLength(5)
            .MaximumLength(20)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.AuthenticationInvalidLoginFailure])
            .WithErrorCode(ErrorCodes.AuthenticationInvalidLoginFailure);
        
        RuleFor(x => x.Password)
            .Matches(@"\d")
            .Matches(@"[a-z]")
            .Matches(@"[A-Z]")
            .MinimumLength(5)
            .MaximumLength(20)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.AuthenticationWeakPasswordFailure])
            .WithErrorCode(ErrorCodes.AuthenticationWeakPasswordFailure);
    }
}