using System.ComponentModel;

namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Represents a direct implementation of <see cref="IFilteringOptions"/>. Options to filter the results from a query.
    /// </summary>
    public class FilteringOptions : SortingAndPagingOptions, IFilteringOptions
    {
        /// <inheritdoc/>
        [DefaultValue("{\n  \"Filters[0].Key\": \"string\",\n  \"Filters[0].Value\": \"string\"\n}")]
        public virtual IDictionary<string, string> Filters { get; set; }

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        public FilteringOptions()
        {
            Filters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="options">Options to build this instance from.</param>
        public FilteringOptions(ISortingAndPagingOptions options) : this()
        {
            MaxResultCount = options?.MaxResultCount ?? IPagingOptions.MaxResultCountLimit;
            SkipCount = options?.SkipCount ?? 0;
            SortingCriteria = options?.SortingCriteria;
        }
    }
}