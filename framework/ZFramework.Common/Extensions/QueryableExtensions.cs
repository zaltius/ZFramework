using ZFramework.Common;
using ZFramework.Common.Exceptions;
using ZFramework.Common.Filtering;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filter, sorts and pages the result of a query and encapsulates the result in a <see cref="IPagedEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the data in the data source.</typeparam>
        /// <param name="query">The query to sort and page.</param>
        /// <param name="filteringOptions">The filtering, sorting and paging options.</param>
        /// <returns>An instance of <see cref="IPagedEnumerable{T}"/> containing the results of the query and paging information.</returns>
        public static IPagedEnumerable<T> PageResult<T>(this IQueryable<T> query, IFilteringOptions? filteringOptions)
        {
            query = query.Filter(filteringOptions);

            return query.PageResult(filteringOptions as ISortingAndPagingOptions);
        }

        /// <summary>
        /// Sorts and pages the result of a query and encapsulates the result in a <see cref="IPagedEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the data in the data source.</typeparam>
        /// <param name="query">The query to sort and page.</param>
        /// <param name="sortingAndPagingOptions">The sorting and paging options.</param>
        /// <returns>An instance of <see cref="IPagedEnumerable{T}"/> containing the results of the query and paging information.</returns>
        public static IPagedEnumerable<T> PageResult<T>(this IQueryable<T> query, ISortingAndPagingOptions? sortingAndPagingOptions)
        {
            var totalCount = query.Count();

            query = query.SortAndPage(sortingAndPagingOptions);

            return new PagedEnumerable<T>()
            {
                TotalCount = totalCount,
                CurrentPage = totalCount > 0 ? (sortingAndPagingOptions?.SkipCount / sortingAndPagingOptions?.MaxResultCount) + 1 : null,
                Items = query.ToList()
            };
        }

        /// <summary>
        /// Sorts and pages the results of a query.
        /// </summary>
        /// <typeparam name="T">The type of the data in the data source.</typeparam>
        /// <param name="query">The query to sort and page.</param>
        /// <param name="sortingAndPagingOptions">The sorting and paging options.</param>
        /// <returns>The result of the query, sorted and paged.</returns>
        public static IQueryable<T> SortAndPage<T>(this IQueryable<T> query, ISortingAndPagingOptions? sortingAndPagingOptions)
        {
            var validOptions = ValidateOptions(sortingAndPagingOptions);

            if (!validOptions)
            {
                throw new FailedValidationException("Opciones de ordenación y/o paginación inválidas.");
            }

            if (sortingAndPagingOptions == null)
            {
                return query;
            }

            try
            {
                query = query.SortBy(sortingAndPagingOptions.SortingCriteria);
                query = query.PageBy(sortingAndPagingOptions.MaxResultCount, sortingAndPagingOptions.SkipCount);

                return query;
            }
            catch (Exception ex)
            {
                throw new FailedValidationException("Opciones de ordenación y/o paginación inválidas.", ex);
            }
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by the given predicate if the given condition is true.
        /// </summary>
        /// <typeparam name="T">The type of the data in the data source.</typeparam>
        /// <param name="query">The query to apply filtering to.</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            Check.NotNull(query, nameof(query));

            return condition
                ? query.Where(predicate)
                : query;
        }

        private static IQueryable<T> Filter<T>(this IQueryable<T> query, IFilteringOptions? filteringOptions)
        {
            Check.NotNull(query, nameof(query));

            if (filteringOptions == null || filteringOptions.Filters.Count == 0)
            {
                return query;
            }

            try
            {
                var columns = string.Join(" and ", filteringOptions.Filters.Select(x => $"{x.Key} == @{filteringOptions.Filters.ToList().IndexOf(x)}").ToArray());

                var values = filteringOptions.Filters.Values.Select(x => x??"").ToArray();

                return query.Where(columns, values);
            }
            catch (Exception ex)
            {
                throw new FailedValidationException("Opciones de filtrado inválidas.", ex);
            }
        }

        private static IQueryable<T> PageBy<T>(this IQueryable<T> query, int maxResultCount, int skipCount = 0)
        {
            Check.NotNull(query, nameof(query));

            return (skipCount < 0 || maxResultCount < 1)
                ? query
                : query.Skip(skipCount).Take(maxResultCount);
        }

        private static IQueryable<T> SortBy<T>(this IQueryable<T> query, string? sortingCriteria)
        {
            Check.NotNull(query, nameof(query));

            return !string.IsNullOrWhiteSpace(sortingCriteria)
               ? query.OrderBy(sortingCriteria)
               : query;
        }

        private static bool ValidateOptions(ISortingAndPagingOptions? sortingAndPagingOptions)
        {
            return sortingAndPagingOptions is null or { MaxResultCount: > 0 and <= IPagingOptions.MaxResultCountLimit, SkipCount: > -1 };
        }
    }
}