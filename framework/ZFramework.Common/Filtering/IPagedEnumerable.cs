namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Represents a collection with paging information. Used as result type for methods who request data with paging.
    /// </summary>
    /// <typeparam name="T">Represents the type of paged data.</typeparam>
    public interface IPagedEnumerable<T>
    {
        /// <summary>
        /// Current page of the requested data.
        /// </summary>
        int? CurrentPage { get; set; }

        /// <summary>
        /// Items in the current page.
        /// </summary>
        IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Total count of the items in every page.
        /// </summary>
        int TotalCount { get; set; }
    }
}