using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Repositories.Contracts
{
    public interface IQuestionRepository : IReadRepository<Question>, IWriteRepository<Question>
    {


    }
}
