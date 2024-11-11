using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace SmallToolsIUse.Linq.Extensions
{
    public static class IQueryableExtensions
    {
        private const string Descending = "desc";
        private const string OrderDescending = "OrderByDescending";
        private const string OrderAscending = "OrderBy";
        private const string ThenByDescending = "ThenByDescending";
        private const string ThenByAscending = "ThenBy";

        public static IQueryable<T> OrderByField<T>(this IQueryable<T> query, string fieldName, string orderDirection)
        {
            if (!string.IsNullOrEmpty(fieldName))
            {
                string method =
                    !string.IsNullOrWhiteSpace(orderDirection) && orderDirection.ToLower().Trim().Equals(Descending)
                        ? OrderDescending
                        : OrderAscending;

                return ApplyOrder<T>(query, fieldName, method);
            }

            return query;
        }

        public static IQueryable<T> ThenByField<T>(this IQueryable<T> query, string fieldName, string orderDirection)
        {
            if (!string.IsNullOrEmpty(fieldName))
            {
                string method =
                !string.IsNullOrWhiteSpace(orderDirection) && orderDirection.ToLower().Trim().Equals(Descending)
                    ? ThenByDescending
                    : ThenByAscending;

                return ApplyOrder<T>(query, fieldName, method);
            }

            return query;
        }

        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> source, string sortBy, string sortDirection, int skip, int take)
        {
            source = source.OrderByField(sortBy, sortDirection);
            return source.Paginate(skip, take);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int skip, int take)
        {
            return source.Skip(skip).Take(take);
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(property))
                {
                    return source.OrderBy(e => e.GetType().GetProperties().FirstOrDefault());
                }

                string[] props = property.Split('.');
                Type type = typeof(T);
                ParameterExpression arg = Expression.Parameter(type, "x");
                Expression expr = arg;

                foreach (string prop in props)
                {
                    // use reflection (not ComponentModel) to mirror LINQ
                    PropertyInfo pi = type.GetProperty(prop,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (pi == null)
                    {
                        pi = type.GetProperties().First();
                        expr = Expression.Property(expr, pi);
                        break;
                    }

                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }

                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

                object result = typeof(Queryable).GetMethods().Single(
                        method => method.Name == methodName
                                  && method.IsGenericMethodDefinition
                                  && method.GetGenericArguments().Length == 2
                                  && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

                return (IOrderedQueryable<T>)result;
            }
            catch
            {
                return (IOrderedQueryable<T>)source;
            }
        }
    }
}
