using Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Specification
{
    public class ProjectIdSpecification : Specification<Project>
    {
        public string ProjectId { get; }

        public ProjectIdSpecification(string projectId)
        {
            ProjectId = projectId;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.Id == ProjectId;
        }
    }
}
