using FormCraft.Business.Contracts.Common;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Responses.Form;

public record FormWithQuestionsResponse(
    string Id,
    string CreatorId,
    FormTypeEnum FormTypeId,
    StatusEnum StatusId,
    List<QuestionResponse> Questions) : IRequest;
