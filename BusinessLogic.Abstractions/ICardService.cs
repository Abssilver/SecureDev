using BusinessLogic.Abstractions.Dto;
using Validation.Abstractions;

namespace BusinessLogic.Abstractions;

public interface ICardService
{
    Task<IOperationResult<IEnumerable<CardDto>>> GetByClientIdAsync(int id);
    Task<IOperationResult<Guid>> CreateAsync(CardDto dto);
}