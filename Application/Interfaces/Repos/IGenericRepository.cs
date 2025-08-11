using Domain.Entities.Common;

public interface IGenericRepository<T, TKey> where T : IEntity<TKey>
{
    Task<T?> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync();
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(TKey id);
    Task SaveChangesAsync();
}


