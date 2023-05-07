namespace SimplePipeline.Rule.Chain
{
    public class FilterPipeline
    {
        private IFilterChain? _previous;
        private IFilterChain? _first;
        private bool _firstExists;
        private readonly FilterPipelineConfiguration _configuration;

        public FilterPipeline(FilterPipelineConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FilterPipeline(Action<FilterPipelineConfiguration> option)
        {
            _configuration = new FilterPipelineConfiguration();
            option?.Invoke(_configuration);
        }

        public void AddFilter(IFilterChain filter)
        {
            filter.SetConfiguration(_configuration);
            if (_previous != null) _previous.SetNext(filter);

            _previous = filter;
            if (!_firstExists)
            {
                _first = filter;
                _firstExists = true;
            }
        }

        public async Task RunAsync(RuleCalculationContext context)
        {
            await _first?.FilterExecuteAsync(context)!;
        }
    }
}
