using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Responses.Answer
{
    public record AnswerResponse(string Id, string Label, string QuestionId) : IRequest;
}
