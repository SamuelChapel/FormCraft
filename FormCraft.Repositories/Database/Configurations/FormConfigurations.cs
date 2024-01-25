using FormCraft.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormCraft.Repositories.Database.Configurations;

public class FormConfigurations : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.HasOne(f => f.Creator)
            .WithMany(c => c.Forms)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
