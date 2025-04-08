using CustomPipeline.Customs.Pipes;
using CustomPipeline.PipeLine;
using CustomPipeline.PipeLine.Core;

namespace CustomPipeline.Customs
{
    public class TestPipeline : PipelinePrebuilt<CustomPipelineContext>
    {
        public override IPipelineBuilder<CustomPipelineContext> Build(IPipelineBuilderFactory factory)
        {
            return factory.Create<CustomPipelineContext>("one")
                .Use<Test1>()
                .Use<Test2>()
                .Use<Test3>();
        }
    }
}
