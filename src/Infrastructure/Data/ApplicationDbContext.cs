using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Transaction> Transaction { get; set; }
}
