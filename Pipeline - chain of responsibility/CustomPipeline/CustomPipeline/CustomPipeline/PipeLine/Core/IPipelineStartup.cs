using System.Threading.Tasks;
namespace CustomPipeline.PipeLine.Core
{
    /// <summary>
    /// 管道启动
    /// </summary>
    public interface IPipelineStartup
    {
        /// <summary>
        /// 管道启动
        /// </summary>
        /// <returns></returns>
        ValueTask StartAsync();
    }
}
