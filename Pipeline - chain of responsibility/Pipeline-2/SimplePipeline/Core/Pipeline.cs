using Autofac;

namespace SimplePipeline.Core
{
    public class Pipeline : IPipeline
    {
        private IEnumerable<StepMap> _steps;
        private PipeLineStatus _status;
        private readonly ILifetimeScope _serviceProvider;

        public Pipeline(ILifetimeScope serviceProvider)
        {
            _steps = new List<StepMap>();
            _status = PipeLineStatus.Prepare;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<StepMap> StepMaps => _steps;

        public bool AddSteps(IEnumerable<StepMap> steps)
        {
            if (_status == PipeLineStatus.Running)
            {
                return false;
            }

            _steps = _steps.Concat(steps);
            return true;
        }

        public async Task ExecuteAsync()
        {
            _status = PipeLineStatus.Running;
            var context = new SalaryContext(_serviceProvider);
            foreach (var step in _steps)
            {
                var stepContext = new SubjectExecutionContext(context, step.SubjectData);
                await step.Step.ExecuteAsync(stepContext);
            }

            _status = PipeLineStatus.End;
        }
    }

    public enum PipeLineStatus
    {
        Running,
        End,
        Prepare
    }
}
