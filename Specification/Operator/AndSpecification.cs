using LinqKit;
using System.Linq.Expressions;

namespace Operator
{
    public struct AndSpecification<T> : ISpecification<T>
    {
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            Right = right;
            Left = left;
        }

        public ISpecification<T> Left { get; }

        public ISpecification<T> Right { get; }

        public bool IsSatisfiedBy(T entity)
        {
            return Left.IsSatisfiedBy(entity) && Right.IsSatisfiedBy(entity);
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return Left.ToExpression().And(Right.ToExpression());
        }
    }
}
