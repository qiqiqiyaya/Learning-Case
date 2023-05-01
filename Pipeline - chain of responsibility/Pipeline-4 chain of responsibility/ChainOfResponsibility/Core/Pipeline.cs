using Autofac;

namespace ChainOfResponsibility.Core
{
    public class Pipeline : IPipeline
    {
        private PipeLineStatus _status;
        private IHandler? _previous;
        private IHandler? _firstHandler;
        private bool _firstExists;

        public Pipeline(ILifetimeScope serviceProvider)
        {
            _status = PipeLineStatus.Prepare;
            Context = new PipelineContext(serviceProvider);
        }

        public PipelineContext Context { get; }

        public bool AddHandler(IHandler handler)
        {
            if (_status == PipeLineStatus.Running)
            {
                throw new Exception("Can't add handlerMap when pipeline is running.");
            }

            if (_previous != null)
            {
                _previous.SetNext(handler);
            }

            _previous = handler;
            if (!_firstExists)
            {
                _firstHandler = handler;
                _firstExists = true;
            }

            return true;
        }

        public async Task RunAsync()
        {
            _status = PipeLineStatus.Running;
            await _firstHandler?.ExecuteAsync(Context)!;
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
