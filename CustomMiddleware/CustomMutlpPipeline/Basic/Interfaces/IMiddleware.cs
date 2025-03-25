namespace CustomMutlpPipeline.Basic.Interfaces
{
	/// <summary>
	/// 中间件
	/// </summary>
	public interface IMiddleware
	{
		ValueTask InvokeAsync(PipelineContext context);
	}
}
