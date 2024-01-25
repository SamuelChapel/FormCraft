using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Responses.Question
{
    public record QuestionResponse(string Id, string Label, string FormId) : IRequest;
}
