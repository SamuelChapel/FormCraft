using FormCraft.Business.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts.Requests.Question
{
    public record UpdateQuestionRequest(
    string Id,
    string? Label = null,
    int? Number = null,
    int? QuestionTypeId = null
    ) : IRequest;

}
