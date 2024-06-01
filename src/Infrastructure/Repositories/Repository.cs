using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Common.Factories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class Repository<T> : IRepository<T>
        where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public Repository(IDbContextFactory factory)
    {
        _context = (ApplicationDbContext)factory.Create();
    }

    protected DbSet<T> Set => _context.Set<T>();

    public IQueryable<T> GetAll()
    {
        return Set.AsQueryable<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Set.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Set.FindAsync(id);
    }

    public async Task<T> GetByIdAsync(string id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await Set.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task AddAsync(T entity)
    {
        await Set.AddAsync(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Set.Update(entity);
        await SaveAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        Set.Remove(entity);
        await SaveAsync();
    }

    public async Task<IEnumerable<T>> ExecuteStoredProcedure(string query)
    {
        return await Set.FromSqlRaw(query).ToListAsync();
    }

    ///////////////////////////// Private Methods ///////////////////////////////
    private async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
