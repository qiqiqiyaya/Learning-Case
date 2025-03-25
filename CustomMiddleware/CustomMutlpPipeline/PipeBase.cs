using CustomMutlpPipeline.Basic;

namespace CustomMutlpPipeline
{
	public abstract class PipeBase : Pipe
	{
		public override async ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next)
		{
			await InvokingAsync(context);
			await next(context);
		}

		protected abstract ValueTask InvokingAsync(PipelineContext context);
	}
}
