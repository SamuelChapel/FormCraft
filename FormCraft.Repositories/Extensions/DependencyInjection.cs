using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Database.Contexts;
using FormCraft.Repositories.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormCraft.Repositories.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("FormCraft"), b => b.MigrationsAssembly("FormCraft.Repositories"));
        });

        services.AddTransient<IAnswerRepository, AnswerRepository>();
        services.AddTransient<IQuestionRepository, QuestionRepository>();
        services.AddTransient<IFormRepository, FormRepository>();

        return services;
    }


}
