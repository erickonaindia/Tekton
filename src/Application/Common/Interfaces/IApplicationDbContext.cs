using Tekton.Domain.Entities;

namespace Tekton.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
