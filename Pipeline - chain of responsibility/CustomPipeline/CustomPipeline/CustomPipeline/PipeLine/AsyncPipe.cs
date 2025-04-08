using CustomPipeline.PipeLine.Core;
using System;
using System.Threading.Tasks;

namespace CustomPipeline.PipeLine
{
    public abstract class AsyncPipe<TContext> : Pipe<TContext>
    {
        /// <summary>
        /// InvokeAsync the functional operation.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="next">The delegate used to invoke the next pipe.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.ValueTask" /> to invoke the functional operation.
        /// </returns>
        public override async ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next)
        {
            await InvokeAsync(context);
            await next(context);
        }

        /// <summary>
        /// InvokeAsync the functional operation synchronously.
        /// </summary>
        /// <param name="context">The execution context.</param>
        protected abstract ValueTask InvokeAsync(TContext context);
    }
}
