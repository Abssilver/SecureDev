using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation.Implementation;

public class CardRepository : ICardRepository
{
    private readonly CardStorageServiceDbContext _context;

    public CardRepository(CardStorageServiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardEntity>> GetAllAsync()
    {
        return await _context.Cards.ToArrayAsync();
    }
    
    public async Task<CardEntity> GetByIdAsync(Guid id)
    {
        var result = await _context.Cards
            .FirstOrDefaultAsync(entity => entity.Id == id) ?? CardEntity.Empty;
        return result;
    }

    public async Task<Guid> CreateAsync(CardEntity entity)
    {
        var client = _context.Clients
            .FirstOrDefault(client => client.Id == entity.ClientId);
        if (client == null)
            return CardEntity.Empty.Id;
        
        var result = _context.Cards.Add(entity);
        await _context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<int> UpdateAsync(CardEntity entity)
    {
        var item = await _context.Cards
            .FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (item == null)
            return 0;
        
        _context.Entry(item).CurrentValues.SetValues(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = new CardEntity { Id = id };
        _context.Cards.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<IEnumerable<CardEntity>> GetByClientId(int clientId)
    {
        var result = await _context.Cards
            .Where(card => card.ClientId == clientId)
            .ToArrayAsync();
        return result;
    }
}