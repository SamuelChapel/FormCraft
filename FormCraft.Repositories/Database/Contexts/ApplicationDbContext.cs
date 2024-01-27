using FormCraft.Entities;
using FormCraft.Entities.Common;
using FormCraft.Repositories.Database.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Form> Forms { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.AddSeeds();

        base.OnModelCreating(builder);
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
