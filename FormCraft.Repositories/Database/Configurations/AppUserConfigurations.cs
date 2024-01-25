using FormCraft.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormCraft.Repositories.Database.Configurations;

public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasMany(u => u.Forms)
            .WithOne(f => f.Creator)
            .HasForeignKey(f => f.CreatorId);
    }
}
