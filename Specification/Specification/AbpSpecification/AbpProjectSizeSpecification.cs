using AbpSpecification;
using System.Linq.Expressions;

namespace Specification.AbpSpecification
{
    public class AbpProjectSizeSpecification : Specification<Project>
    {
        public int Size { get; set; }

        public AbpProjectSizeSpecification(int size)
        {
            Size = size;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.Size == Size;
        }
    }
}
