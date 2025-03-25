namespace CustomMutlpPipeline.Basic
{
	/// <summary>
	/// 管道中的一环
	/// </summary>
	public abstract class Pipe
	{
		public abstract ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next);
	}
}
