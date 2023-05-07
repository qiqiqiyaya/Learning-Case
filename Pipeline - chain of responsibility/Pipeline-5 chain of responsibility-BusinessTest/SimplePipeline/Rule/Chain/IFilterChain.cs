namespace SimplePipeline.Rule.Chain
{
    /// <summary>
    /// 过滤器链
    /// </summary>
    public interface IFilterChain
    {
        /// <summary>
        /// 设置下一个过滤器
        /// </summary>
        /// <param name="filter"></param>
        void SetNext(IFilterChain filter);

        /// <summary>
        /// 设置过滤器管道配置
        /// </summary>
        /// <param name="configuration"></param>
        void SetConfiguration(FilterPipelineConfiguration configuration);

        /// <summary>
        /// 执行过滤器
        /// </summary>
        /// <returns></returns>
        Task FilterExecuteAsync(RuleCalculationContext context);
    }
}
