namespace DAL.Abstractions;

public interface IRepository<TEntity, TId>
    where TEntity: class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(TId id);
    Task<TId> CreateAsync(TEntity entity);
    Task<int> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(TId id);
}