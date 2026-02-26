namespace MovieRatingsBackendWebApi.Infrastructure.Exceptions;

public class EntityDeletionException : Exception
{
    /// <summary>
    /// Gets the ID of the entity that could not be deleted.
    /// </summary>
    public int EntityId { get; }

    // Base constructor
    public EntityDeletionException(int entityId, string? message = null, Exception? innerException = null)
        : base(message ?? $"An error occurred while deleting the entity with ID {entityId}.", innerException)
    {
        this.EntityId = entityId;
    }

    public EntityDeletionException(int entityId, Exception innerException)
        : this(entityId, message: null, innerException)
    {
    }
}