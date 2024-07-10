namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Options to filter the results of a query.
    /// </summary>
    public interface IFilteringOptions : ISortingAndPagingOptions
    {
        /// <summary>
        /// Dictionary used for filtering.
        /// Each KeyValuePair defines a new filter in which the key represents the property to filter and the value represents the desired matching value.
        /// FromQuery sending example: { "Filters[0].Key": "Column", "Filters[0].Value": "Value" }
        /// </summary>
        IDictionary<string, string> Filters { get; set; }
    }
}