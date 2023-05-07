namespace SimplePipeline.Rule.SalaryCalculateBehavior
{
    public interface ICalculate
    {
        /// <summary>
        /// 是否需要计算
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> NeedCalculateAsync(RuleCalculationContext context);

        /// <summary>
        /// 计算并返回结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<RuleCalculateResult> CalculateAsync(RuleCalculationContext context);
    }
}
