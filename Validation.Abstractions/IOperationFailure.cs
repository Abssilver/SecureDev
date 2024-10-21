namespace Validation.Abstractions;

public interface IOperationFailure
{
    string PropertyName { get; }
    string Description { get; }
    string ErrorCode { get; }
}