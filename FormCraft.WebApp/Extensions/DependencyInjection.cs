using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;
using FormCraft.WebApp.ViewModels.FormViewModels;

namespace FormCraft.WebApp.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApp(this IServiceCollection services)
    {
        services.AddAutoMapper(conf =>
        {
            conf.CreateMap<FormResponse, FormIndexViewModel>();
            conf.CreateMap<FormWithQuestionsResponse, FormDetailsViewModel>();
        });

        return services;
    }
}
