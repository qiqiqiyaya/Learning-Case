using CustomPipeline.PipeLine;
using System;
using System.Threading.Tasks;

namespace CustomPipeline.Customs.Pipes
{
    class Test1 : AsyncPipe<CustomPipelineContext>
    {
        public override string Description { get; } = "Test1";

        protected override ValueTask InvokeAsync(CustomPipelineContext context)
        {
            Console.Write("this is test1");
            return ValueTask.CompletedTask;
        }
    }
}
