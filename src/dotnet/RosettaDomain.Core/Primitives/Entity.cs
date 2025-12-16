using RosettaDomain.Core.Abstractions;
using RosettaDomain.Core.Exceptions;

namespace RosettaDomain.Core.Primitives
{
    /// <summary>
    /// Abstract base class for entities.
    /// Implements equality logic based on the unique identity (Id) rather than reference or attribute values.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// The setter is protected to ensure encapsulation, allowing setting only within the domain or ORM.
        /// </summary>
        public virtual TId Id { get; init; } = default!;

        /// <summary>
        /// Protected constructor for ORMs or serialization frameworks.
        /// </summary>
        protected Entity() { }

        /// <summary>
        /// Constructor to initialize the entity with an Id.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        protected Entity(TId id)
        {
            Id = id;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        /// <inheritdoc />
        public bool Equals(Entity<TId>? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            // Transient entities (Id is default) are only equal if they are the same reference.
            if (
                EqualityComparer<TId>.Default.Equals(Id, default)
                || EqualityComparer<TId>.Default.Equals(other.Id, default)
            )
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // If the entity is transient (no Id), we use the base HashCode to ensure uniqueness in collections.
            if (EqualityComparer<TId>.Default.Equals(Id, default))
                return base.GetHashCode();

            return Id!.GetHashCode();
        }

        /// <summary>
        /// Compares two entities for equality.
        /// </summary>
        /// <param name="a">The first entity.</param>
        /// <param name="b">The second entity.</param>
        /// <returns>True if both are null or have the same Identity; otherwise, false.</returns>
        public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Compares two entities for inequality.
        /// </summary>
        /// <param name="a">The first entity.</param>
        /// <param name="b">The second entity.</param>
        /// <returns>True if they have different Identities or one is null; otherwise, false.</returns>
        public static bool operator !=(Entity<TId>? a, Entity<TId>? b)
        {
            return !(a == b);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        /// <summary>
        /// Validates a business rule.
        /// If the rule is broken, a BusinessRuleValidationException is thrown.
        /// </summary>
        /// <param name="rule">The rule to validate.</param>
        /// <exception cref="BusinessRuleValidationException">Thrown when the rule is broken.</exception>
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
