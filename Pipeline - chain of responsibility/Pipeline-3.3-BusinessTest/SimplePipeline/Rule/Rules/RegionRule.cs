using SimplePipeline.Scope;

namespace SimplePipeline.Rule.Rules
{
    public class RegionRule : Rule
    {
        public List<string> Code { get; set; } = new List<string>()
        {
            "越南","泰国"
        };

        public override Task<bool> IsSatisfy(Employee employee, ScopeCheckContext context)
        {
            return Task.FromResult(true);
        }

        public override Task<RuleCalculateResult> CalculateAsync(RuleCalculationContext context)
        {
            //context.Employee.
            // 判断 当前员工 是否是 越南或者泰国
            // 如果是 true


            return Task.FromResult(new RuleCalculateResult());
        }


    }
}