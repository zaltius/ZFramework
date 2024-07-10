using ZFramework.Domain.Entities;
using System.Linq.Expressions;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Filtering
{
    internal static class GlobalFiltersHelper
    {
        internal static Expression<Func<TEntity, bool>>? CreateFilterExpression<TEntity>(bool isSoftDeleteFilterEnabled)
            where TEntity : class
        {
            if (ShouldFilterEntity<TEntity>() && isSoftDeleteFilterEnabled)
            {
                return d => !((ISoftDelete)d).IsDeleted;
            }

            return null;
        }

        internal static bool ShouldFilterEntity<TEntity>()
            where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }
    }
}