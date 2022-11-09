using Operator;
using System.Linq.Expressions;

namespace Specification
{
    public class ProjectTypeSpecification : Specification<Project>
    {
        public ProjectType Type { get; set; }

        public ProjectTypeSpecification(ProjectType type)
        {
            Type = type;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.ProjectType == Type;
        }
    }
}
