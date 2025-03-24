using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CustomMiddleware
{
    static class MiddlewareExtensions
    {
        public static PipelineBuilder Use
            (this PipelineBuilder builder, Func<PipelineContext, ValueTask> delegateFunc)
        {
            return builder.Use(new BlankPipe(delegateFunc));
        }

        public static PipelineBuilder Use<
                [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TMiddleware>
            (this PipelineBuilder builder, params object[] args)
            where TMiddleware : IMiddleware
        {
            return builder.Use(new BlankPipe(CreateMiddlewareDelegateFactory<TMiddleware>(builder, args)));
        }

        public static Func<PipelineContext, ValueTask> CreateMiddlewareDelegateFactory<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TMiddleware>(
            this PipelineBuilder builder, params object[] args)
            where TMiddleware : IMiddleware
        {
            var middleware = typeof(TMiddleware);

            var methodInfo = MiddlewareHelpers.GetInvokeMethod(middleware);
            var instance = ActivatorUtilities.CreateInstance(builder.ServiceProvider, middleware, args);

            return (Func<PipelineContext, ValueTask>)methodInfo
                .CreateDelegate(typeof(Func<PipelineContext, ValueTask>), instance);
        }
    }
}
