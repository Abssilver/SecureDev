using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation.Implementation;

public class ClientRepository : IClientRepository
{
    private readonly CardStorageServiceDbContext _context;

    public ClientRepository(CardStorageServiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClientEntity>> GetAllAsync()
    {
        return await _context.Clients.ToArrayAsync();
    }
    
    public async Task<ClientEntity> GetByIdAsync(int id)
    {
        var result = await _context.Clients
            .FirstOrDefaultAsync(entity => entity.Id == id) ?? ClientEntity.Empty;
        return result;
    }

    public async Task<int> CreateAsync(ClientEntity entity)
    {
        var result = _context.Clients.Add(entity);
        await _context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<int> UpdateAsync(ClientEntity entity)
    {
        var item = await _context.Clients
            .FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (item == null)
            return 0;
        
        _context.Entry(item).CurrentValues.SetValues(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = new ClientEntity { Id = id };
        _context.Clients.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }
}