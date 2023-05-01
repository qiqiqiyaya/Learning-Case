using Autofac;

namespace SimplePipeline.Core
{
    public class Pipeline : IPipeline<BasePipeLineContext>
    {
        private IEnumerable<HandlerMap> _handlerMaps;
        private PipelineStatus _status;
        private readonly ILifetimeScope _serviceProvider;
        private IHeadHandler _headHandler;

        public Pipeline(ILifetimeScope serviceProvider)
        {
            _handlerMaps = new List<HandlerMap>();
            _status = PipelineStatus.Prepare;
            _serviceProvider = serviceProvider;
            PipelineContext = new BasePipeLineContext(_serviceProvider);
        }

        public IEnumerable<HandlerMap> HandlerMaps => _handlerMaps;

        public BasePipeLineContext PipelineContext { get; }

        public bool AddHeadHandler(IHeadHandler headHandler)
        {
            _headHandler = headHandler;
            return true;
        }

        public bool AddHandlers(IEnumerable<HandlerMap> handlerMaps)
        {
            // 流水线线运行时，不能向其添加处理器
            if (_status == PipelineStatus.Running)
            {
                return false;
            }

            _handlerMaps = _handlerMaps.Concat(handlerMaps);
            return true;
        }

        public async Task RunAsync()
        {
            _status = PipelineStatus.Running;
            _headHandler?.ExecuteAsync();
            foreach (var map in _handlerMaps)
            {
                var stepContext = new SubjectExecutionContext(PipelineContext, map.Data);
                await map.Handler.ExecuteAsync(stepContext);
            }

            _status = PipelineStatus.End;
        }
    }

    public enum PipelineStatus
    {
        Running,
        End,
        Prepare
    }
}
