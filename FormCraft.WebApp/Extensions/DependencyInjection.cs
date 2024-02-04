using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.WebApp.Models;
using FormCraft.WebApp.ViewModels.FormViewModels;

namespace FormCraft.WebApp.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApp(this IServiceCollection services)
    {
        services.AddAutoMapper(conf =>
        {
            conf.CreateMap<FormResponse, CreateFormModel>();
            conf.CreateMap<FormResponse, FormResponseViewModel>();
            conf.CreateMap<FormWithQuestionsResponse, FormDetailsViewModel>();
            conf.CreateMap<SearchFormResponse, FormResponseViewModel>();
        });

        return services;
    }
}
