using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> innerQuery, ISpecification<TEntity> spec)
        {
            var query = innerQuery;
            if (spec.Critria is not null)
            {
                query = query.Where(spec.Critria);
            }
            if (spec.OrderBy is not null)
            {
                query=query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)
            {
                query=query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.IsPaginationEnable)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
