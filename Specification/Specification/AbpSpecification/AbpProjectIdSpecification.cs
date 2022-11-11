using AbpSpecification;
using System.Linq.Expressions;

namespace Specification.AbpSpecification
{
    public class AbpProjectIdSpecification : Specification<Project>
    {
        public string Id { get; }

        public AbpProjectIdSpecification(string id)
        {
            Id = id;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.Id == Id;
        }
    }
}
