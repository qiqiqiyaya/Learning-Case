using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CustomPipeline.PipeLine.Core
{
    internal class PipelineBuilder<TContext> : IPipelineBuilder<TContext>
    {
        private readonly List<Type> _pipeTypes = new List<Type>();

        public PipelineBuilder(string pipelineName)
        {
            PipelineName = pipelineName;
        }

        public string PipelineName { get; set; }

        public IPipelineBuilder<TContext> Use<TPipe>()
            where TPipe : Pipe<TContext>
        {
            _pipeTypes.Add(typeof(TPipe));
            return this;
        }

        public Func<TContext, ValueTask> Build(IServiceProvider serviceProvider, out PipeDescriptorInfo exportedPipeline)
        {
            List<Pipe<TContext>> list = new List<Pipe<TContext>>();

            PipeDescriptorInfo nextDescriptor = PipeDescriptorInfo.Terminal;
            for (int index = _pipeTypes.Count - 1; index > -1; index--)
            {
                var type = _pipeTypes[index];
                var pipe = (Pipe<TContext>)ActivatorUtilities.CreateInstance(serviceProvider, type);
                nextDescriptor = pipe.Export(nextDescriptor);
                list.Add(pipe);
            }
            exportedPipeline = nextDescriptor;

            list.Reverse();
            return PipelineBuilder<TContext>.Build(list);
        }

        private static Func<TContext, ValueTask> Build(IList<Pipe<TContext>> pipes)
        {
            Func<TContext, ValueTask> nextPipeline = _ => new ValueTask();
            for (int index = pipes.Count - 1; index > -1; index--)
            {
                var pipe = pipes[index];
                var convertedPipe = Convert(pipe);
                nextPipeline = convertedPipe(nextPipeline);
            }
            return nextPipeline;
        }

        private static Func<Func<TContext, ValueTask>, Func<TContext, ValueTask>> Convert(Pipe<TContext> pipe)
        {
            return next => context => InvokeAsync(pipe, context, next);
        }

        private static ValueTask InvokeAsync(Pipe<TContext> pipe, TContext context, Func<TContext, ValueTask> next)
        {
            if (context is ICancellableContext cancellableContext)
            {
                cancellableContext.CancellationToken.ThrowIfCancellationRequested();
            }
            if (context is IAbortableContext abortableContext && abortableContext.IsAborted)
            {
                return new ValueTask();
            }
            return pipe.InvokeAsync(context, next);
        }
    }
}