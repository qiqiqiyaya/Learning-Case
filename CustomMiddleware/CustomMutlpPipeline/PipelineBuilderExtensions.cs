using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMutlpPipeline
{
    public static class PipelineBuilderExtensions
    {
        public static PipelineBuilder AddPipeline(this IServiceCollection collection, Action<IPipelineProvider> builderAction)
        {

        }
    }
}
