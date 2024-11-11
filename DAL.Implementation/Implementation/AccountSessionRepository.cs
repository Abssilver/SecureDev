using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation.Implementation;

public class AccountSessionRepository : IAccountSessionRepository
{
    private readonly CardStorageServiceDbContext _context;

    public AccountSessionRepository(CardStorageServiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountSessionEntity>> GetAllAsync()
    {
        return await _context.AccountSessions.ToArrayAsync();
    }
    
    public async Task<AccountSessionEntity> GetByIdAsync(int id)
    {
        var result = await _context.AccountSessions
            .FirstOrDefaultAsync(entity => entity.Id == id) ?? AccountSessionEntity.Empty;
        return result;
    }

    public async Task<int> CreateAsync(AccountSessionEntity entity)
    {
        var result = _context.AccountSessions.Add(entity);
        await _context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<int> UpdateAsync(AccountSessionEntity entity)
    {
        var item = await _context.AccountSessions
            .FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (item == null)
            return 0;
        
        _context.Entry(item).CurrentValues.SetValues(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = new AccountSessionEntity { Id = id };
        _context.AccountSessions.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }
}