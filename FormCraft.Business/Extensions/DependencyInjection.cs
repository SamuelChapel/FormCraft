using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Contracts.Responses.Question;
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
            conf.CreateMap<CreateAnswerRequest, Answer>();
            conf.CreateMap<DeleteAnswerRequest, Answer>();
            conf.CreateMap<Answer, DeleteAnswerRequest>();
            conf.CreateMap<UpdateAnswerRequest, Answer>();

            conf.CreateMap<Question, QuestionResponse>();
            conf.CreateMap<CreateQuestionRequest, Question>();
            conf.CreateMap<DeleteQuestionRequest, Question>();
            conf.CreateMap<Question, DeleteQuestionRequest>();
            conf.CreateMap<UpdateQuestionRequest, Question>();

            conf.CreateMap<Form, FormResponse>();
            conf.CreateMap<CreateFormRequest, Form>();
            conf.CreateMap<DeleteFormRequest, Form>();
            conf.CreateMap<Form, DeleteFormRequest>();
            conf.CreateMap<UpdateFormRequest, Form>();
        });

        return services;
    }
}
