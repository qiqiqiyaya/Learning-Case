using CustomPipeline.PipeLine;
using CustomPipeline.PipeLine.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CustomPipeline
{
    public class PipeRegisterBuilder
    {
        public static PipeRegisterBuilder Create()
        {
            return new PipeRegisterBuilder();
        }

        private readonly Dictionary<string, object> _pipeBuilder = new();
        private readonly PipelineStartups _startupCollection = new();

        public void AddPipeLine<TPipelineStartup, TPipelinePrebuilt, TContext>()
            where TPipelinePrebuilt : PipelinePrebuilt<TContext>, new()
            where TPipelineStartup : IPipelineStartup
        {
            var prebuilt = new TPipelinePrebuilt();
            var pipelineBuilder = prebuilt.Build(new PipelineBuilderFactory());

            _startupCollection.Add(pipelineBuilder.PipelineName, typeof(TPipelineStartup));
            _pipeBuilder.Add(pipelineBuilder.PipelineName, pipelineBuilder);
        }

        public void Build(IServiceCollection services)
        {
            services.AddSingleton<IPipelineProvider>(serviceProvider => new PipelineProvider(serviceProvider, _pipeBuilder));
            services.AddSingleton(_startupCollection);

            foreach (var pipe in _startupCollection)
            {
                services.AddTransient(pipe.Value);
            }
        }
    }
}
