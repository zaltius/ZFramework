namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Represents a direct implementation of <see cref="ISortingAndPagingOptions"/>. Options to sort and page the results from a query.
    /// </summary>
    public class SortingAndPagingOptions : PagingOptions, ISortingAndPagingOptions
    {
        /// <inheritdoc/>
        public virtual string? SortingCriteria { get; set; }
    }
}