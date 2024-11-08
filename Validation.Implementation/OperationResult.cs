using Validation.Abstractions;

namespace Validation.Implementation;

public class OperationResult<TResult>: IOperationResult<TResult>
{
    public TResult Result { get;}
    public IReadOnlyList<IOperationFailure> Failures { get; }
    public bool IsSucceed => Failures.Count == 0;

    public OperationResult(TResult result, IReadOnlyList<IOperationFailure> failures)
    {
        Result = result;
        Failures = failures;
    }
    
    public OperationResult(TResult result, IOperationFailure failure)
        : this(result, new List<IOperationFailure>{ failure })
    {
    }
}