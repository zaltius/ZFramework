namespace ZFramework.Common.Filtering
{
    /// <summary>
    /// Options to page results from a query.
    /// </summary>
    public interface IPagingOptions
    {
        /// <summary>
        /// Max result count limit.
        /// </summary>
        public const int MaxResultCountLimit = 100;

        /// <summary>
        /// Max amount of returned results.
        /// </summary>
        int MaxResultCount { get; set; }

        /// <summary>
        /// Amount of registers to skip.
        /// </summary>
        int SkipCount { get; set; }
    }
}