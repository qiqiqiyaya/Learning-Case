namespace Operator
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> Not<T>(this ISpecification<T> @this)
        {
            return new NotSpecification<T>(@this);
        }
    }
}
