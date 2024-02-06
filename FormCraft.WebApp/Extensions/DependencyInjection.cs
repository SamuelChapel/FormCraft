using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;
using FormCraft.WebApp.Models;
using FormCraft.WebApp.ViewModels;
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
            conf.CreateMap<FormWithQuestionsResponse, CreateFormModel>();
            conf.CreateMap<SearchFormResponse, FormResponseViewModel>();

            conf.CreateMap<QuestionResponse, QuestionDetailsViewModel>()
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
        });

        return services;
    }
}
