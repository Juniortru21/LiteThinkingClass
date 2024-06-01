using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Infrastructure.Common.Factories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class StoredProcedure<T>(IDbContextFactory factory) : IStoredProcedure<T>
        where T : notnull
{
    private readonly ApplicationDbContext _context = (ApplicationDbContext)factory.Create();

    public async Task<IEnumerable<T>> ExecuteStoredProcedureQuery(string query)
    {
        return await _context.Database.SqlQueryRaw<T>(query).ToListAsync();
    }

    public async Task<T> ExecuteStoredProcedureQueryFirst(string query)
    {
        return await _context.Database.SqlQueryRaw<T>(query).FirstAsync();
    }

    public async Task<int> ExecuteStoredProcedureCommand(string query)
    {
        return await _context.Database.ExecuteSqlRawAsync(query);
    }
}
