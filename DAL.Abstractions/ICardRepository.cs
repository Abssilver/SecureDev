using DAL.Abstractions.Entities;

namespace DAL.Abstractions;

public interface ICardRepository: IRepository<CardEntity, Guid>
{
    Task<IEnumerable<CardEntity>> GetByClientId(int clientId);
}