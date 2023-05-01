namespace SimplePipeline.Core
{
    /// <summary>
    /// pipeline's Builder
    /// </summary>
    public interface IPipelineBuilder<TData>
    {
        IPipeline<IPipelineContext, TData> Build(List<Subject> data);
    }
}
