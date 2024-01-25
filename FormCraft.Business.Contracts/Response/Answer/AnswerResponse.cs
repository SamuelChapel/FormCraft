using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using FormCraft.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts.Response.Answer
{
    public record AnswerResponse(string Id, string Label, string QuestionId) : IRequest;
}
