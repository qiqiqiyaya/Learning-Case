using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkflow
{
    public interface IWorkflow : IActivity
    {
        public List<IActivity> ActivityContainer { get; }

        public Task Run();

        public IWorkflow Then(IActivity activity);
    }
}
