namespace SimplePipeline.Rule.Chain
{
    public abstract class RuleFilterChain : IFilterChain
    {
        private IFilterChain _next = default!;
        protected FilterPipelineConfiguration Configuration { get; private set; } = default!;

        public void SetNext(IFilterChain filter)
        {
            _next = filter;
        }

        public void SetConfiguration(FilterPipelineConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract Task FilterExecuteAsync(RuleCalculationContext context);

        protected virtual async Task Next(RuleCalculationContext context)
        {
            Configuration.BeforeExecuteNextFilter?.Invoke(context);
            await _next?.FilterExecuteAsync(context)!;
        }
    }
}
