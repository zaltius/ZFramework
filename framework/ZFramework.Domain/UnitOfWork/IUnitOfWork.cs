namespace ZFramework.Domain.UnitOfWork
{
    /// <summary>
    /// Represents a Unit of Work to work in a transactional-like fashion.
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Saves all pending changes.
        /// </summary>
        /// <returns>True if the commit succeeds. False otherwise.</returns>
        void Commit();

        /// <summary>
        /// Asynchronously saves all pending changes.
        /// </summary>
        /// <returns>True if the commit succeeds. False otherwise.</returns>
        Task CommitAsync();

        /// <summary>
        /// Rolls back all pending changes.
        /// </summary>
        void Rollback();
    }
}