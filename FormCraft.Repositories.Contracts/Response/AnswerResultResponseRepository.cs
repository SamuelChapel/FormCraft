using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Repositories.Contracts.Response
{
    public record AnswerResultResponseRepository(string AnswerId, int Pickcount);
}