using DemoAPI.Common.Interfaces;
using System.Linq.Expressions;


namespace DemoAPI.Common
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public bool IsPaginationEnabled { get; set; }

        public int Skip { get; set; }
        
        public int Take { get; set; }

        public BaseSpecifications() { 

        }

        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
