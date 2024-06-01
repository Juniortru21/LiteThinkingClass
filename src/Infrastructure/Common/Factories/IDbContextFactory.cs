using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Factories;
public interface IDbContextFactory
{
    DbContext Create();
}
