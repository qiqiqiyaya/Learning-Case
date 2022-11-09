using Operator;
using System.Linq.Expressions;

namespace Specification
{
    public class ProjectSizeSpecification : Specification<Project>
    {
        public int Size { get; set; }

        public ProjectSizeSpecification(int size)
        {
            Size = size;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.Size == Size;
        }
    }
}
