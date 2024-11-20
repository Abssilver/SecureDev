using FluentValidation;
using Validation.Abstractions;

namespace Validation.Implementation;

public abstract class ValidationWrapper<TEntity> : AbstractValidator<TEntity>, IValidationService<TEntity> 
    where TEntity : class
{
    public IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item)
    {
        var result = base.Validate(item);
        if (result == null || result.Errors.Count == 0)
            return ArraySegment<IOperationFailure>.Empty;

        return result.Errors.Select(x => new OperationFailure
        {
            PropertyName = x.PropertyName,
            Description = x.ErrorMessage,
            ErrorCode = x.ErrorCode
        }).ToList();
    }
}