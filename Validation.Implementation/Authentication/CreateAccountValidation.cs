using Authentication.Abstractions.Dto;
using FluentValidation;

namespace Validation.Implementation.Authentication;

public class CreateAccountValidation: ValidationWrapper<CreateAccountDto>
{
    public CreateAccountValidation()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.CreateUserEmailFailure])
            .WithErrorCode(ErrorCodes.CreateUserEmailFailure);
        
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .Matches(@"[a-z]")
            .MinimumLength(2)
            .MaximumLength(30)
            .NotEqual("undefined")
            .NotEqual("null")
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.CreateUserFirstNameFailure])
            .WithErrorCode(ErrorCodes.CreateUserFirstNameFailure);
        
        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty()
            .Matches(@"[a-z]")
            .MinimumLength(2)
            .MaximumLength(30)
            .NotEqual("undefined")
            .NotEqual("null")
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.CreateUserSurnameFailure])
            .WithErrorCode(ErrorCodes.CreateUserSurnameFailure);
        
        RuleFor(x => x.Patronymic)
            .NotEqual("undefined")
            .NotEqual("null")
            .MaximumLength(30)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.CreateUserPatronymicFailure])
            .WithErrorCode(ErrorCodes.CreateUserPatronymicFailure);
        
        RuleFor(x => x.Password)
            .Matches(@"\d")
            .Matches(@"[a-z]")
            .Matches(@"[A-Z]")
            .MinimumLength(5)
            .MaximumLength(20)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.CreateUserWeakPasswordFailure])
            .WithErrorCode(ErrorCodes.CreateUserWeakPasswordFailure);
    }
}