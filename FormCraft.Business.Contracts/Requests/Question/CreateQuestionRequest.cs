using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts.Requests.Question
{
    public record CreateQuestionRequest(
          int Number,
          string Label,
          int QuestionTypeId,
          string FormId
        ) : IRequest;
}

