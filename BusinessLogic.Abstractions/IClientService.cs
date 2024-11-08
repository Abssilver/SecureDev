using BusinessLogic.Abstractions.Dto;
using Validation.Abstractions;

namespace BusinessLogic.Abstractions;

public interface IClientService
{
    Task<IOperationResult<int>> CreateAsync(ClientDto dto);
}