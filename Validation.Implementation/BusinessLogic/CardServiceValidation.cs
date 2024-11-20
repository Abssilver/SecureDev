using System.Globalization;
using BusinessLogic.Abstractions.Dto;
using FluentValidation;

namespace Validation.Implementation.BusinessLogic;

public class CardServiceValidation: ValidationWrapper<CardDto>
{
    public CardServiceValidation()
    {
        RuleFor(x => x.CardNumber)
            .CreditCard()
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.CardCreationCardNumberFailure])
            .WithErrorCode(ErrorCodes.CardCreationCardNumberFailure);
        
        RuleFor(x => x.ExpDate)
            .Must(date => 
                DateTime.TryParseExact(date, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) 
                && result > DateTime.UtcNow)
            .WithMessage(ErrorCodes.ErrorCodeDescription[ErrorCodes.AuthenticationWeakPasswordFailure])
            .WithErrorCode(ErrorCodes.AuthenticationWeakPasswordFailure);
    }
}