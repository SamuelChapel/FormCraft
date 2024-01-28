using FormCraft.Business.Contracts.Common;
using FormCraft.Business.Contracts.Responses.Question;

namespace FormCraft.Business.Contracts.Responses.Form;

public record FormWithQuestionsResponse(
    string Id,
    string CreatorId,
    int FormTypeId,
    int StatusId,
    List<QuestionResponse> Questions) : IRequest;
