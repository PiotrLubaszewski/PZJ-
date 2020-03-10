using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Models.Collections;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;

namespace Timesheet.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public async static Task<ICollectionResult<V>> ToCollectionResultAsync<T, V>(this IQueryable<T> query, OperationQuery operationQuery, CancellationToken cancellationToken) where T : class where V : class
        {
            return (await query.ToCollectionResultAsync(operationQuery, cancellationToken)).Adapt<ICollectionResult<V>>();
        }

        public async static Task<ICollectionResult<T>> ToCollectionResultAsync<T>(this IQueryable<T> query, OperationQuery operationQuery, CancellationToken cancellationToken) where T : class
        {
            var totalCount = query.Count();

            operationQuery ??= new OperationQuery();

            if (!string.IsNullOrEmpty(operationQuery.Sort))
            {
                query = OrderBy(query, operationQuery.Sort);
            }

            operationQuery.PageSize = operationQuery.PageSize > 0 ? operationQuery.PageSize : 25;
            operationQuery.CurrentPage = operationQuery.CurrentPage > 0 ? operationQuery.CurrentPage : 1;

            var pagesCount = (totalCount + operationQuery.PageSize - 1) / operationQuery.PageSize;
            var currentPage = operationQuery.CurrentPage > pagesCount ? pagesCount : operationQuery.CurrentPage;
            var internalCurrentPage = currentPage > 0 ? currentPage : 1;

            var result = await query.Skip(operationQuery.PageSize * (internalCurrentPage - 1)).Take(operationQuery.PageSize).ToListAsync(cancellationToken);

            return new CollectionResult<T>
            {
                Data = result,
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PagesCount = pagesCount
            };
        }

        private static IQueryable<V> OrderBy<V>(IQueryable<V> query, string sortExpression)
        {
            if (query == null)
                throw new ArgumentNullException("source", "source is null.");

            sortExpression = sortExpression.First().ToString().ToUpper() + sortExpression.Substring(1);
            sortExpression = sortExpression.Replace('_', ' ');
            var parts = sortExpression.Split(' ');
            var isDescending = false;
            var tType = typeof(V);

            if (parts.Length <= 0 || parts[0] == "")
                return query;

            var propertyName = parts[0];

            if (parts.Length > 1)
                isDescending = parts[1].ToLower().Contains("desc");

            var prop = tType.GetProperty(propertyName);

            if (prop == null)
                return query;

            var funcType = typeof(Func<,>)
                .MakeGenericType(tType, prop.PropertyType);

            var lambdaBuilder = typeof(Expression)
                .GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            var parameter = Expression.Parameter(tType);
            var propExpress = Expression.Property(parameter, prop);

            var sortLambda = lambdaBuilder
                .Invoke(null, new object[] { propExpress, new ParameterExpression[] { parameter } });

            var sorter = typeof(Queryable)
                .GetMethods()
                .FirstOrDefault(x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2)
                .MakeGenericMethod(new[] { tType, prop.PropertyType });

            return (IQueryable<V>)sorter
                .Invoke(null, new object[] { query, sortLambda });
        }
    }
}
