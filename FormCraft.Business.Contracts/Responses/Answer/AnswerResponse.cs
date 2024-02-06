using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Responses.Answer;

public record AnswerResponse(
    string Id,
    string Label,
    string QuestionId,
    int Pickcount = 0) : IRequest;
