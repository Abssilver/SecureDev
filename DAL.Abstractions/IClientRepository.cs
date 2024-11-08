using DAL.Abstractions.Entities;

namespace DAL.Abstractions;

public interface IClientRepository: IRepository<ClientEntity, int>
{
    
}