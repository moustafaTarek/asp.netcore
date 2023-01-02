using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }

        public int Skip { get; }
        public int Take { get; }
        public bool IsPagingEnabled { get; }
    }
}