namespace ZFramework.Common.Filtering
{
    public static class PagedEnumerableExtensions
    {
        /// <summary>
        /// Execute a the given function to every item in a PagedEnumerable.
        /// </summary>
        /// <typeparam name="TIn">Type of the source collection.</typeparam>
        /// <typeparam name="TOut">Destination type to create.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="mapFunc">Mapping function.</param>
        /// <returns>Mapped destination object.</returns>
        public static IPagedEnumerable<TOut> Map<TIn, TOut>(this IPagedEnumerable<TIn> source, Func<TIn, TOut> mapFunc)
        {
            return new PagedEnumerable<TOut>()
            {
                CurrentPage = source.CurrentPage,
                Items = source.Items.Select(mapFunc),
                TotalCount = source.TotalCount
            };
        }
    }
}