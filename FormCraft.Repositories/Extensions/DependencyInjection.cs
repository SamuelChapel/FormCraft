using FormCraft.Repositories.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormCraft.Repositories.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("FormCraft"));
        });

        return services;
    }
}
