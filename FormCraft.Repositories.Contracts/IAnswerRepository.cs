using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;

namespace FormCraft.Repositories.Contracts
{
    public interface IAnswerRepository : IReadRepository<Answer> ,IWriteRepository<Answer>
    {
    }
}
