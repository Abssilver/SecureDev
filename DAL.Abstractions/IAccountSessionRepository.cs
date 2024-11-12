using DAL.Abstractions.Entities;

namespace DAL.Abstractions;

public interface IAccountSessionRepository: IRepository<AccountSessionEntity, int>
{
    Task<AccountSessionEntity> GetByTokenAsync(string token);
}