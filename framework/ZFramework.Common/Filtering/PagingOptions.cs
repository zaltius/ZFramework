namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Represents a default implementation of <see cref="IPagingOptions"/>. Options to page results from a query.
    /// </summary>
    public class PagingOptions : IPagingOptions
    {
        /// <summary>
        /// <inheritdoc/> Default value is 10. Check <see cref="IPagingOptions.MaxResultCountLimit"/> for max result count limit.
        /// </summary>
        public virtual int MaxResultCount { get; set; } = 10;

        /// <inheritdoc/>
        public virtual int SkipCount { get; set; }
    }
}