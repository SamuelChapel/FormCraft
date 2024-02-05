using FormCraft.Business.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts.Requests.Answer
{
    public record CreateUserAnswerRequest(string AppUserId, string AnswerId) : IRequest;
}
