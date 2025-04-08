using System;
using System.Collections.Generic;

namespace CustomPipeline.PipeLine.Core
{
    internal class PipelineProvider : IPipelineProvider
    {
        private readonly Dictionary<string, object> _pipelineBuilders;
        private readonly Dictionary<string, PipeDescriptorInfo> _exportedPipelines = new();
        private readonly IServiceProvider _serviceProvider;

        public PipelineProvider(IServiceProvider serviceProvider, Dictionary<string, object> pipelineBuilders)
        {
            _serviceProvider = serviceProvider;
            _pipelineBuilders = pipelineBuilders;
        }

        public IPipeline<TContext> GetPipeline<TContext>(string name)
        {
            if (_pipelineBuilders.TryGetValue(name, out var value))
            {
                if (value is IPipelineBuilder<TContext> builder)
                {
                    var func = builder.Build(_serviceProvider, out PipeDescriptorInfo descriptor);

                    IPipeline<TContext> pipeline = new Pipeline<TContext>(func);
                    _exportedPipelines[name] = descriptor;
                    return pipeline;
                }
            }

            throw new ArgumentException("The specified name does not match pipeline execution context type.", nameof(name));
        }

        public IDictionary<string, PipeDescriptorInfo> ExportAllPipelines() => _exportedPipelines;

        //public bool TryGetPipeline(string name, out PipelineRegistration pipeline)
        //{
        //	if (_pipelineBuilders.TryGetValue(name, out var value))
        //	{
        //		if (value is PipelineRegistration result)
        //		{
        //			pipeline = result;
        //			return true;
        //		}

        //		throw new ArgumentException("The specified name does not match pipeline execution context type.", nameof(name));
        //	}

        //	pipeline = null;
        //	return false;
        //}
    }
}
