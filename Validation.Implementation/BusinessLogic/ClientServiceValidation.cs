using BusinessLogic.Abstractions.Dto;
using FluentValidation;

namespace Validation.Implementation.BusinessLogic;

public class ClientServiceValidation: ValidationWrapper<ClientDto>
{
    public ClientServiceValidation()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .Matches(@"[a-z]")
            .MinimumLength(2)
            .MaximumLength(30)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.ClientCreationFirstNameFailure])
            .WithErrorCode(ErrorCodes.ClientCreationFirstNameFailure);
        
        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty()
            .Matches(@"[a-z]")
            .MinimumLength(2)
            .MaximumLength(30)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.ClientCreationSurnameFailure])
            .WithErrorCode(ErrorCodes.ClientCreationSurnameFailure);
    }
}