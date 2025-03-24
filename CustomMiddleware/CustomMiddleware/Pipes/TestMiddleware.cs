using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMiddleware.Pipes
{
    class TestMiddleware : IMiddleware
    {
        public ValueTask InvokeAsync(PipelineContext context)
        {
            Console.WriteLine($"{nameof(TestMiddleware)}");
            return ValueTask.CompletedTask;
        }
    }
}
