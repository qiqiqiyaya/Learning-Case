using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule.Rules
{
    /// <summary>
    /// 符合资格员工
    /// </summary>
    public class EligibleEmployeeRule : Rule, ICondition, ICalculate
    {
        public List<string> EmployeeCodes { get; set; } = default!;

        public override async Task FilterExecuteAsync(RuleCalculationContext context)
        {
            await RelationshipHandle(context);
            // TODO 此处可以做其他事
        }

        public override Task<bool> IsSatisfy(RuleCalculationContext context)
        {
            return Task.FromResult(EmployeeCodes.Contains(context.Employee.Code));
        }

        public Task<RuleCalculateResult> CalculateAsync(RuleCalculationContext context)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> NeedCalculateAsync(RuleCalculationContext context)
        {
            return Task.FromResult(true);
        }
    }
}