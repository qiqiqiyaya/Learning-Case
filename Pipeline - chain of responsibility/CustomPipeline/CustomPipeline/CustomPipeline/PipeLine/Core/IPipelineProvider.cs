using System.Collections.Generic;

namespace CustomPipeline.PipeLine.Core
{
    /// <summary>
    ///  Represents the provider to register and get named pipeline.
    /// </summary>
    public interface IPipelineProvider
    {
        IPipeline<TContext> GetPipeline<TContext>(string name);

        /// <summary>Exports all pipelines.</summary>
        /// <returns>The dictionary containing all named pipeline based <see cref="PipeDescriptorInfo"/>.</returns>
        IDictionary<string, PipeDescriptorInfo> ExportAllPipelines();
    }
}
