using Autofac;

namespace SimplePipeline.Core
{
    public class Pipeline : IPipeline
    {
        private IEnumerable<HandlerMap> _handlerMaps;
        private PipeLineStatus _status;
        private readonly ILifetimeScope _serviceProvider;

        public Pipeline(ILifetimeScope serviceProvider)
        {
            _handlerMaps = new List<HandlerMap>();
            _status = PipeLineStatus.Prepare;
            _serviceProvider = serviceProvider;
            Context = new PipelineContext(_serviceProvider);
        }

        public IEnumerable<HandlerMap> Maps => _handlerMaps;

        public PipelineContext Context { get; }

        public bool AddHandlerMaps(IEnumerable<HandlerMap> handlerMaps)
        {
            if (_status == PipeLineStatus.Running)
            {
                throw new Exception("Can't add handlerMap when pipeline is running.");
            }

            _handlerMaps = _handlerMaps.Concat(handlerMaps);
            return true;
        }

        public async Task RunAsync()
        {
            _status = PipeLineStatus.Running;
            foreach (var map in _handlerMaps)
            {
                var stepContext = new HandlerExecutionContext(Context, map.Subject);
                await map.Handler.ExecuteAsync(stepContext);
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
