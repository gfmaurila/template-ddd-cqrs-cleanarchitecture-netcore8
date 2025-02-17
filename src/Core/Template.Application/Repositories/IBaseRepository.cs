using Template.Common.Domain;

namespace Template.Application.Repositories;

/// <summary>
/// Represents a generic base repository interface for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="T">The type of the entity, which must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Creates a new entity in the repository.
    /// </summary>
    /// <param name="obj">The entity to be created.</param>
    /// <returns>The created entity.</returns>
    Task<T> Create(T obj);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="obj">The entity with updated data.</param>
    /// <returns>The updated entity.</returns>
    Task<T> Update(T obj);

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="obj">The entity to be removed.</param>
    Task Remove(T obj);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified ID, or null if not found.</returns>
    Task<T> Get(Guid id);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    Task<List<T>> Get();
}
