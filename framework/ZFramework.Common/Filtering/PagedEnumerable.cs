namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Represents a direct implementation of <see cref="IPagedEnumerable{T}"/>. Used as result type for methods who request data with paging.
    /// </summary>
    /// <typeparam name="T">Represents the type of paged data.</typeparam>
    public class PagedEnumerable<T> : IPagedEnumerable<T>
    {
        /// <inheritdoc/>
        public int? CurrentPage { get; set; }

        /// <inheritdoc/>
        public IEnumerable<T> Items { get; set; }

        /// <inheritdoc/>
        public int TotalCount { get; set; }

        /// <summary>
        /// Creates an instance of this class. Builds an empty list of items.
        /// </summary>
        public PagedEnumerable()
        {
            Items = new List<T>();
        }
    }
}