using System.Diagnostics.CodeAnalysis;
using CustomMutlpPipeline.Basic;
using CustomMutlpPipeline.Basic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMutlpPipeline.Extensions
{
	public static class MiddlewareExtensions
	{
		public static IPipelineBuilder Use
			(this IPipelineBuilder builder, Func<PipelineContext, ValueTask> delegateFunc)
		{
			return builder.Use(new BlankPipe(delegateFunc));
		}

		public static IPipelineBuilder Use<
				[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TMiddleware>
			(this IPipelineBuilder builder, params object[] args)
			where TMiddleware : IMiddleware
		{
			return builder.Use(new BlankPipe(builder.CreateMiddlewareDelegateFactory<TMiddleware>(args)));
		}

		public static Func<PipelineContext, ValueTask> CreateMiddlewareDelegateFactory<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TMiddleware>(
			this IPipelineBuilder builder, params object[] args)
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
