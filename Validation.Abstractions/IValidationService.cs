namespace Validation.Abstractions;

public interface IValidationService<in TEntity> where TEntity : class
{
    IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
}