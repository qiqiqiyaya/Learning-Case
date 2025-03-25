using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomMutlpPipeline.Basic;
using CustomMutlpPipeline.Basic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMutlpPipeline
{
	public static class PipelineRegisterExtensions
	{
		public static void AddPipeline(this IServiceCollection collection, Action<IPipelineContainer> builderAction)
		{
			collection.AddTransient<IPipelineBuilder, PipelineBuilder>();
			collection.AddSingleton(serviceProvider =>
			{
				var provider = new PipelineProvider(serviceProvider);
				builderAction(provider);

				// 自动扫描

				return provider;
			});
			collection.AddSingleton<IPipelineProvider>(serviceProvider => serviceProvider.GetRequiredService<PipelineProvider>());
			collection.AddSingleton<IPipelineContainer>(serviceProvider => serviceProvider.GetRequiredService<PipelineProvider>());
		}
	}
}
