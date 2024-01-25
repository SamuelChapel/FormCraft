using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;

namespace FormCraft.Repositories.Contracts
{
    public interface IQuestionRepository : IReadRepository<Question>, IWriteRepository<Question>
    {
    }
}
