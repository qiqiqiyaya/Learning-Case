using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimplePipeline
{
    public interface IPipelineBuilder<T>
        where T : IDataContext
    {
        IPipeline CreatePipeline(T dataContext);
    }
}
