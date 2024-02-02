using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;

namespace FormCraft.Repositories.Contracts
{
    public interface IFormRepository : IReadRepository<Form>, IWriteRepository<Form>
    {
        /// <summary>
        /// Search method to find with string input
        /// </summary>
        /// <param name="search"></param>
        /// <returns>returns the <typeparamref name="TEntity"/> if it's found</returns>
        Task<List<Form>> Search(string[]? type, string[]? status, string? label, int? order, string? currentUserId);
    }
}
