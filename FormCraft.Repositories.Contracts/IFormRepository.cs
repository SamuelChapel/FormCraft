using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;

namespace FormCraft.Repositories.Contracts
{
    public interface IFormRepository : IReadRepository<Form>, IWriteRepository<Form> { }
}
