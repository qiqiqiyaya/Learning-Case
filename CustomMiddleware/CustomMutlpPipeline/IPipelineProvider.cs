namespace CustomMutlpPipeline
{
    public interface IPipelineProvider
    {
        public void AddPipeline(string name, Action<PipelineBuilder> builderAction);
        public Pipeline GetPipeline(string name);
    }
}
