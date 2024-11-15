using FormCraft.Repositories.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.WebApp.Extensions;

public static class MigrationExtensions
{
    public static void AddMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
