namespace Validation.Abstractions;

public interface IOperationResult<out TResult>
{
    TResult? Result { get; }
    IReadOnlyList<IOperationFailure> Failures { get; }
    bool IsSucceed { get; }
}