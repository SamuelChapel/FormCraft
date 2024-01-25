using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Response.Answer;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Response.Question;
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
            conf.CreateMap<DeleteQuestionRequest, Answer>();
            conf.CreateMap<Answer, DeleteQuestionRequest>();
            conf.CreateMap<UpdateAnswerRequest, Answer>();

            conf.CreateMap<Question, QuestionResponse>();
            conf.CreateMap<CreateQuestionRequest, Question>();
            conf.CreateMap<DeleteQuestionRequest, Question>();
            conf.CreateMap<Question, DeleteQuestionRequest>();
            conf.CreateMap<UpdateQuestionRequest, Question>();
        });

        return services;
    }
}
