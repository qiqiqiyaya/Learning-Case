using CustomPipeline.PipeLine;
using System;
using System.Threading.Tasks;

namespace CustomPipeline.Customs.Pipes
{
    class Test2 : AsyncPipe<CustomPipelineContext>
    {
        public override string Description { get; } = "Test2";

        protected override ValueTask InvokeAsync(CustomPipelineContext context)
        {
            Console.Write("this is test2");
            return ValueTask.CompletedTask;
        }
    }
}
