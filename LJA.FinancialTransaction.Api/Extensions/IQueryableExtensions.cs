using System.Linq.Expressions;

namespace LJA.FinancialTransaction.Api.Extensions
{
    public static class IQueryableExtensions
    {
        // Conditionally applies a Where clause to an IQueryable<T> based on a boolean condition.
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
