namespace MovieRatingsBackendWebApi.Repositories.Core;

using System.Linq.Expressions;

public interface IRepository<T>
    where T : class
{
    /// <summary>
    /// Determines whether any entity of type T exists that satisfies a condition specified by the predicate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>The task result contains true if any entities match the predicate; otherwise, false.</returns>
    /// <remarks>
    /// This method is optimized for performance with a focus on existence checks rather than retrieving entities. 
    /// Including related entities (eager loading) is not applicable here as the method's purpose is to quickly check 
    /// for the existence of entities matching the predicate without the need to work directly with the entities themselves 
    /// or their related data. This ensures efficient execution and minimizes unnecessary data retrieval.
    /// </remarks>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);

    void Add(T entity);

    /// <summary>
    /// Deletes an entity by its ID. This method is intentionally synchronous because it marks the entity for deletion in the context without
    /// performing immediate database operations. The actual database interaction is deferred until the changes are committed to the database,
    /// which can be done using an appropriate commit method. This design choice avoids unnecessary asynchronous overhead for operations that
    /// do not directly involve I/O tasks, providing flexibility in managing when and how database transactions are executed.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    bool Delete(int id);

    void DeleteRange(IEnumerable<T> entities);

    T? FindById(int? id, string? includeProperties = null);

    /// <summary>
    ///     Constructs a query to find all entities of type <typeparamref name="T"/> that match the specified filter criteria.
    ///     This method returns an <see cref="IQueryable{T}"/> allowing for further query composition before execution.
    /// </summary>
    /// <param name="filter">
    ///     An expression to filter entities. Only entities satisfying this condition are included in the result.
    /// </param>
    /// <param name="includeProperties">
    ///     Optional. A comma-separated list of navigation properties for eager loading. Including related entities can be
    ///     achieved by specifying their names here, which helps in fetching complex data structures efficiently in a single query.
    /// </param>
    /// <returns>
    ///     An <see cref="IQueryable{T}"/> representing the query for entities of type <typeparamref name="T"/> that match
    ///     the filter criteria. The query has not been executed at this point, allowing callers to further refine the query
    ///     with additional LINQ operations (such as sorting, paging, and projecting) before materializing the results with
    ///     an execution method (e.g., ToListAsync).
    /// </returns>
    /// <remarks>
    ///     This method is intentionally designed to be synchronous and return an <see cref="IQueryable{T}"/> to provide
    ///     maximum flexibility in query composition. It enables the caller to append further LINQ operations and decide
    ///     the optimal point to execute the query asynchronously, for instance, by calling <c>ToListAsync()</c> on the
    ///     resulting <see cref="IQueryable{T}"/>. Keeping this method synchronous avoids prematurely materializing the
    ///     query results, thereby enhancing efficiency and allowing for more expressive queries tailored to specific needs.
    /// </remarks>
    IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, string? includeProperties = null);

    /// <summary>
    ///     Retrieves a single entity based on the specified filter criteria, with optional inclusion of related entities through eager loading.
    /// </summary>
    /// <param name="filter">Expression to filter entities. Ensures only entities satisfying this condition are considered.</param>
    /// <param name="includeProperties">Comma-separated list of navigation properties for eager loading. Optional.</param>
    /// <returns>
    ///     The task result contains the matching entity, or null if none found.
    ///     If the criteria match more than one entity, an <see cref="InvalidOperationException"/> is thrown.
    /// </returns>
    [Obsolete("TODO: This method is no longer used. Should it be deleted?")]
    Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);

    /// <summary>
    ///     Retrieves all entities of type <typeparamref name="T"/> from the database.
    ///     Allows for optional eager loading of related entities specified by navigation properties.
    /// </summary>
    /// <param name="includeProperties">
    ///     An optional comma-separated list of navigation properties for eager loading. When specified, this parameter
    ///     allows for the inclusion of related entities in the query result, facilitating operations on complex data structures.
    /// </param>
    /// <returns>
    ///     The task result contains a collection of entities of type <typeparamref name="T"/>.
    ///     The query is not executed until the collection is enumerated.
    ///     If <paramref name="includeProperties"/> is specified, related entities will be included.
    /// </returns>
    Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);

    IQueryable<T> GetAsQueryable();
}
