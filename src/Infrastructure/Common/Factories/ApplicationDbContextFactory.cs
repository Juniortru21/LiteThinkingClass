using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Common.Factories;
public class ApplicationDbContextFactory : IDbContextFactory
{
    private readonly DbContextOptions<ApplicationDbContext> options;

    public ApplicationDbContextFactory(DbContextOptions<ApplicationDbContext> options)
    {
        this.options = options;
    }

    public DbContext Create()
    {
        return new ApplicationDbContext(this.options);
    }
}
