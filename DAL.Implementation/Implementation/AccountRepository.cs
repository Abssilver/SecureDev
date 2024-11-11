using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation.Implementation;

public class AccountRepository : IAccountRepository
{
    private readonly CardStorageServiceDbContext _context;

    public AccountRepository(CardStorageServiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountEntity>> GetAllAsync()
    {
        return await _context.Accounts.ToArrayAsync();
    }
    
    public async Task<AccountEntity> GetByIdAsync(int id)
    {
        var result = await _context.Accounts
            .FirstOrDefaultAsync(entity => entity.Id == id) ?? AccountEntity.Empty;
        return result;
    }

    public async Task<int> CreateAsync(AccountEntity entity)
    {
        var result = _context.Accounts.Add(entity);
        await _context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<int> UpdateAsync(AccountEntity entity)
    {
        var item = await _context.Accounts
            .FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (item == null)
            return 0;
        
        _context.Entry(item).CurrentValues.SetValues(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = new AccountEntity { Id = id };
        _context.Accounts.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<AccountEntity> GetByEmail(string email)
    {
        var result = await _context.Accounts
            .SingleOrDefaultAsync(card => card.Email == email) ?? AccountEntity.Empty;
        return result;
    }
}