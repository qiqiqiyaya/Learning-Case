using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule.Rules
{
    /// <summary>
    /// 金额
    /// </summary>
    public class AmountRule : Rule , ICalculate, IDeclare
    {
        public decimal Amount { get; set; }

        public string CurrencyUnit { get; set; }


        public Task<RuleCalculateResult> CalculateAsync(RuleCalculationContext context)
        {


            throw new NotImplementedException();
        }

        public Task DeclareOperation()
        {
            throw new NotImplementedException();
        }
    }
}
