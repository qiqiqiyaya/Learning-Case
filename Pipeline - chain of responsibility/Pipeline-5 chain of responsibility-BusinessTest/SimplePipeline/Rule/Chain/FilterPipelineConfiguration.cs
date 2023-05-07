namespace SimplePipeline.Rule.Chain
{
    public class FilterPipelineConfiguration
    {
        public Func<RuleCalculationContext, Task> BeforeExecuteNextFilter { get; set; } = default!;
    }
}
