using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule.Rules
{
    /// <summary>
    /// 组织
    /// </summary>
    public class OrganizationRule : Rule, ICondition
    {
        public Task<bool> IsSatisfy(RuleCalculationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
