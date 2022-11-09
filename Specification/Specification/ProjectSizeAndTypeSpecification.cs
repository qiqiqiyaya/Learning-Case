using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Operator;

namespace Specification
{
    public class ProjectSizeAndTypeSpecification : Specification<Project>
    {
        public int Size { get; set; }

        public ProjectType ProjectType { get; set; }

        public ProjectSizeAndTypeSpecification(int size, ProjectType type)
        {
            Size = size;
            ProjectType = type;
        }

        public override Expression<Func<Project, bool>> ToExpression()
        {
            return project => project.Size == Size && project.ProjectType == ProjectType;
        }
    }
}
