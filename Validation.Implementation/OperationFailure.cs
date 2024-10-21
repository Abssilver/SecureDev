using Validation.Abstractions;

namespace Validation.Implementation;

public sealed class OperationFailure : IOperationFailure
{
    public string PropertyName { get; set; }
    public string Description { get; set; }
    public string ErrorCode { get; set; }
}