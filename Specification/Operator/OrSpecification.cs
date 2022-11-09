using System.Linq.Expressions;
using LinqKit;

namespace Operator
{
    public struct OrSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _left.IsSatisfiedBy(entity) || _right.IsSatisfiedBy(entity);
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return _left.ToExpression().Or(_right.ToExpression());
        }
    }
}
