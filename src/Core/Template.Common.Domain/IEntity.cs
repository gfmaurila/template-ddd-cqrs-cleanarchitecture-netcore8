namespace Template.Common.Domain;

/// <summary>
/// Marker interface to represent an entity.
/// </summary>
public interface IEntity
{
}

/// <summary>
/// Marker interface to represent an entity with a strongly-typed key.
/// </summary>
/// <typeparam name="TKey">The type of the entity's key.</typeparam>
public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    TKey Id { get; }
}