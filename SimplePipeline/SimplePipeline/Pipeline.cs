namespace SimplePipeline
{
    public class Pipeline<T> : IPipeline 
        where T : IDataContext
    {
        private readonly List<IStep<T>> _steps = new List<IStep<T>>();
        private readonly T _dataContext;

        public Pipeline(T dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddSteps(IEnumerable<IStep<T>> steps)
        {
            _steps.AddRange(steps);
        }

        public async Task ExecuteAsync()
        {
            foreach (var step in _steps)
            {
                await step.ExecuteAsync(_dataContext);
            }
        }
    }
}
