using AbpSpecification;
using System.Linq.Expressions;

namespace Specification.AbpSpecification
{
    public class AbpProjectTypeSpecification : Specification<Project>
    {
        public ProjectType Type { get; set; }

        public AbpProjectTypeSpecification(ProjectType type)
        {
            Type = type;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.ProjectType == Type;
        }
    }
}
