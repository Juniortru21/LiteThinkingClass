using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Transaction> Transaction { get; set;  }
}
