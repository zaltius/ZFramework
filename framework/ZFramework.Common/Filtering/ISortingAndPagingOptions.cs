namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Options to sort and page the results from a query.
    /// </summary>
    public interface ISortingAndPagingOptions : IPagingOptions
    {
        /// <summary>
        /// Criteria used for sorting the results. I.e., "[PropertyName] DESC"
        /// </summary>
        string? SortingCriteria { get; set; }
    }
}