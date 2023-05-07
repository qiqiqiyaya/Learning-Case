namespace SimplePipeline.Rule.SalaryCalculateBehavior
{
    /// <summary>
    /// 表示该 <see cref="Rule"/> 是一个条件
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> IsSatisfy(RuleCalculationContext context);
    }
}
