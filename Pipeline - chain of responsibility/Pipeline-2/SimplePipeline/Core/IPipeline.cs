namespace SimplePipeline.Core
{
    public interface IPipeline
    {
        IEnumerable<StepMap> StepMaps { get; }

        public bool AddSteps(IEnumerable<StepMap> steps);

        Task ExecuteAsync();
    }
}
