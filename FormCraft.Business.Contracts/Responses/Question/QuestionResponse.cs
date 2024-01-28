using FormCraft.Business.Contracts.Common;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Responses.Question;

public record QuestionResponse(
    string Id,
    string Label,
    int Number,
    QuestionTypeEnum QuestionTypeId,
    string FormId,
    List<AnswerResponse> Answers) : IRequest;
