using FormCraft.Entities.Common;

namespace FormCraft.Repositories.Contracts.Common;

public interface IReadRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Get a <typeparamref name="TEntity"/> by his id 
    /// </summary>
    /// <param name="id"> <seealso cref="Guid"/> of the <typeparamref name="TEntity"/> id to find</param>
    /// <returns>returns the <typeparamref name="TEntity"/> if it's found</returns>
    Task<TEntity?> GetById(Guid id);

    /// <summary>
    /// Method for getting all the <typeparamref name="TEntity"/> 
    /// </summary>
    /// <returns>returns the <seealso cref="List{TEntity}"/></returns>
    Task<List<TEntity>> GetAll();
}
