using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; } = false;

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> CritriaExpression)
        {
            Critria = CritriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderByExp)
        {
            OrderBy=OrderByExp;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByExp)
        {
            OrderByDesc=OrderByExp;
        }

        public void ApplyPagination(int skip,int take) 
        {
            IsPaginationEnable=true;
            Skip=skip;
            Take=take;
        }
    }
}
