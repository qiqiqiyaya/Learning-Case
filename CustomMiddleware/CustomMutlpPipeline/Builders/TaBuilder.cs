using CustomMutlpPipeline.Basic;
using CustomMutlpPipeline.Extensions;
using CustomMutlpPipeline.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMutlpPipeline.PipelineBuilders
{
	class TaBuilder : SpecifyPipelineBuilder
	{
		public override void Build(PipelineProvider pipelineProvider)
		{
			pipelineProvider.Add("test", builder =>
			{
				builder.Use<TestMiddleware>();
				builder.Use(context =>
				{
					Console.WriteLine("test2");
					return ValueTask.CompletedTask;
				});
			});
		}
	}
}
