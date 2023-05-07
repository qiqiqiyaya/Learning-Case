using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule.Rules
{
    /// <summary>
    /// 地区
    /// </summary>
    public class RegionRule : Rule, ICondition
    {
        public List<string> Code { get; set; } = new List<string>()
        {
            "越南","泰国"
        };

        public Task<bool> IsSatisfy(RuleCalculationContext context)
        {
            //context.Employee.
            // 判断 当前员工 是否是 越南或者泰国
            // 如果是 true
            throw new NotImplementedException();
        }
    }
}