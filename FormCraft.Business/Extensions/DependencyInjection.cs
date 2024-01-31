using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Business.Services;
using FormCraft.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FormCraft.Business.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddAutoMapper(conf =>
        {
            conf.CreateMap<Answer, AnswerResponse>();
            conf.CreateMap<Answer, DeleteAnswerRequest>().ReverseMap();
            conf.CreateMap<CreateAnswerRequest, Answer>();
            conf.CreateMap<UpdateAnswerRequest, Answer>();

            conf.CreateMap<Question, QuestionResponse>();
            conf.CreateMap<Question, DeleteQuestionRequest>().ReverseMap();
            conf.CreateMap<CreateQuestionRequest, Question>();
            conf.CreateMap<UpdateQuestionRequest, Question>();

            conf.CreateMap<Form, FormResponse>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator!.UserName));

            conf.CreateMap<Form, FormWithQuestionsResponse>();
            conf.CreateMap<Form, DeleteFormRequest>().ReverseMap();
            conf.CreateMap<CreateFormRequest, Form>();
            conf.CreateMap<UpdateFormRequest, Form>();

            conf.CreateMap<SearchFormRequest, Form>();
            conf.CreateMap<Form, SearchFormResponse>();

        });
        services.AddTransient<IAnswerBusiness, AnswerBusiness>();
        services.AddTransient<IQuestionService, QuestionService>();
        services.AddTransient<IFormBusiness, FormBusiness>();




        return services;
    }
}
