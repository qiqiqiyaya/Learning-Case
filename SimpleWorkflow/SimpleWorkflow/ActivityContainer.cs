using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkflow
{
    public class ActivityContainer
    {
        private readonly List<IActivity> _container;

        public ActivityContainer()
        {
            _container = new List<IActivity>();
        }

        // 向流程中添加一个步骤
        public void Insert(IActivity activity)
        {
            _container.Add(activity);
        }

        public void Popup()
        {
            //_container.
        }
    }
}
