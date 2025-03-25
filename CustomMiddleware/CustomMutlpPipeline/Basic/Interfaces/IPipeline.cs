namespace CustomMutlpPipeline.Basic.Interfaces
{
	/// <summary>
	/// 管道
	/// </summary>
	public interface IPipeline
	{
		string Name { get; }

		ValueTask ExecuteAsync(PipelineContext context);
	}
}
