using DAL.Abstractions.Entities;

namespace DAL.Abstractions;

public interface IAccountRepository: IRepository<AccountEntity, int>
{
    Task<AccountEntity> GetByEmail(string email);
}