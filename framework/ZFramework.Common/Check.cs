using ZFramework.Common.Exceptions;

namespace ZFramework.Common
{
    /// <summary>
    /// Static class to do some miscellaneous cheks.
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Checks if the given value is null./>
        /// </summary>
        /// <typeparam name="T">Represents the type of the value to evaluate.</typeparam>
        /// <param name="value">Item to evaluate.</param>
        /// <param name="parameterName">Name of the provided value used to throw the exception in case it is null.</param>
        /// <returns>The given value if not null. Otherwise, throws an <see cref="ArgumentException"/>.</returns>
        public static T NotNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new NullValueException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Checks if the given entity is null./>
        /// </summary>
        /// <typeparam name="TEntity">Represents the type of the entity to evaluate.</typeparam>
        /// <param name="entity">Entity to evaluate.</param>
        /// <param name="message">Optional message to display if the entity is null. If not provided, a default message is used.</param>
        /// <returns>The given entity if not null. Otherwise, throws a <see cref="NullEntityException"/>.</returns>
        public static TEntity NotNullEntity<TEntity>(TEntity entity, string? message = null)
        {
            if (entity == null)
            {
                if (message == null)
                {
                    message = $"La entidad {typeof(TEntity).Name} no existe.";
                }

                throw new NullEntityException(message);
            }

            return entity;
        }

        /// <summary>
        /// Checks if the given entity is null./>
        /// </summary>
        /// <typeparam name="TEntity">Represents the type of the entity to evaluate.</typeparam>
        /// <typeparam name="TPrimaryKey">Represents the unique id of the entity to evaluate.</typeparam>
        /// <param name="entity">Entity to evaluate.</param>
        /// <param name="id">Unique id of the entity to evaluate.</param>
        /// <param name="message">Optional message to display if the entity is null. If not provided, a default message is used.</param>
        /// <returns>The given entity if not null. Otherwise, throws a <see cref="NullEntityException"/>.</returns>
        public static TEntity NotNullEntity<TEntity, TPrimaryKey>(TEntity entity, TPrimaryKey id, string? message = null)
        {
            if (entity == null)
            {
                if (message == null)
                {
                    var userFriendlyMessage = $"La entidad {typeof(TEntity).Name} no existe";
                    message = $"La entidad {typeof(TEntity).Name} con ID {id} no existe.";

                    throw new NullEntityException(userFriendlyMessage, new NullEntityException(message));
                }
                else
                {
                    throw new NullEntityException(message);
                }
            }

            return entity;
        }
    }
}