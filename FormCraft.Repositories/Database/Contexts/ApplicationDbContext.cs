using FormCraft.Entities;
using FormCraft.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker
            .Entries()
            .Where(e => e.Entity is IDated && e.State is EntityState.Added or EntityState.Modified)
            .ToList()
            .ForEach(e =>
            {
                if (e.State is EntityState.Added)
                    ((IDated)e.Entity).CreatedAt = DateTime.UtcNow;

                ((IDated)e.Entity).UpdatedAt = DateTime.UtcNow;
            });

        return base.SaveChangesAsync(cancellationToken);
    }
}
