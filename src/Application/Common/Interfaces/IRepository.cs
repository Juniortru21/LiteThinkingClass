using Domain.Entities;

namespace Application.Common.Interfaces;
public interface IRepository<T>
    where T : BaseEntity
{
    IQueryable<T> GetAll();

    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task<T> GetByIdAsync(string id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task RemoveAsync(T entity);

    Task<IEnumerable<T>> ExecuteStoredProcedure(string query);
}
