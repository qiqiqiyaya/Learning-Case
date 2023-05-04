using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePipeline.Core;

namespace SimplePipeline.Rule.Rules
{
    public class AmountRule : Rule
    {
        public decimal Amount { get; set; }

        public string CurrencyUnit { get; set; }

        public override Task<RuleCalculateResult> CalculateAsync(RuleCalculationContext context)
        {
            return Task.FromResult(new RuleCalculateResult() { RuleCalculateResultType = RuleCalculateResultType.Numerical, Value = Amount });
        }
    }
}
