using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkflow
{
    public abstract class Composite : Activity.Activity
    {
        public ICollection<IActivity> Activities { get; set; } = new HashSet<IActivity>();
    }
}
