using CustomPipeline.PipeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPipeline.Customs.Pipes
{
    class Test3 : AsyncPipe<CustomPipelineContext>
    {
        public override string Description { get; } = "Test3";

        protected override ValueTask InvokeAsync(CustomPipelineContext context)
        {
            Console.Write("this is test3");
            return ValueTask.CompletedTask;
        }
    }
}
