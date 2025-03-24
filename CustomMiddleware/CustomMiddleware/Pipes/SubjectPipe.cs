using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMiddleware.Pipes
{
    class SubjectPipe : Pipe
    {
        public override ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next)
        {
            Console.WriteLine("SubjectPipe");
            return next(context);
        }
    }
}
