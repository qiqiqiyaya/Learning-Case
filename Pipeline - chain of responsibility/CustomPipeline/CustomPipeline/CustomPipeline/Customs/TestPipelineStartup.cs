using CustomPipeline.PipeLine.Core;
using System.Threading.Tasks;

namespace CustomPipeline.Customs
{
    class TestPipelineStartup(IPipelineProvider pipelineProvider) : IPipelineStartup
    {
        public async ValueTask StartAsync()
        {
            var pipeline = pipelineProvider.GetPipeline<CustomPipelineContext>("one");
            await pipeline.ProcessAsync(new CustomPipelineContext());
        }
    }
}
