using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Requests.Question;

public record CreateQuestionRequest(
      int Number,
      string Label,
      int QuestionTypeId,
      string FormId
    ) : IRequest;

