using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CursoUdemy.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> dictionary)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !dictionary.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(dictionary[queryObj.SortBy]);
            else
                return query.OrderByDescending(dictionary[queryObj.SortBy]);
        }
    }
}
