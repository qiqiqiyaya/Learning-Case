using Autofac;

namespace SimplePipeline.Core
{
    public class Pipeline : IPipeline
    {
        private PipeLineStatus _status;
        private readonly ILifetimeScope _serviceProvider;
        private readonly IHandlerFactory _handlerFactory;

        public Pipeline(ILifetimeScope serviceProvider, IHandlerFactory handlerFactory)
        {
            _status = PipeLineStatus.Prepare;
            _serviceProvider = serviceProvider;
            _handlerFactory= handlerFactory;
        }

        public PipelineContext Context { get; private set; } = default!;

        public PipelineConfiguration Configuration { get; private set; } = default!;

        public PipeLineStatus Status => _status;

        /// <summary>
        /// Pipeline initialization
        /// </summary>
        /// <param name="configuration"></param>
        public void Init(PipelineConfiguration configuration)
        {
            Context = new PipelineContext(_serviceProvider, configuration.Employee);
            Configuration = configuration;
        }

        public async Task RunAsync()
        {
            _status = PipeLineStatus.Running;
            foreach (var sub in Configuration.Subjects)
            {
                var handler = _handlerFactory.Create(sub);
                await handler.ExecuteAsync(new HandlerExecutionContext(Context, sub));
            }

            _status = PipeLineStatus.End;
        }
    }
}
