using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePipeline.Rule
{
    public class RuleCalculateService : IRuleCalculateService
    {
        public async Task CalculateAsync(RuleCalculationContext context)
        {
            var trees = context.GetRuleTrees();

            foreach (var tree in trees)
            {
                if (!await tree.IsSatisfy(context.Employee, context))
                {
                    continue;
                }

                switch (tree.Behavior)
                {
                    case ExecuteBehavior.Done:
                        continue;
                    case ExecuteBehavior.Continue:
                        if (tree.HasChildren)
                        {
                            await ChildrenCalculateAsync(tree.Children, context);
                        }
                        break;
                    case ExecuteBehavior.CalculateThenDone:
                        await tree.CalculateAsync(context);
                        continue;
                }
            }
        }

        public async Task ChildrenCalculateAsync(List<Rule> rules, RuleCalculationContext context)
        {
            foreach (var rule in rules)
            {
                if (!await rule.IsSatisfy(context.Employee, context))
                {
                    continue;
                }

                switch (rule.Behavior)
                {
                    case ExecuteBehavior.Done:
                        continue;
                    case ExecuteBehavior.Continue:
                        if (rule.HasChildren)
                        {
                            await ChildrenCalculateAsync(rule.Children, context);
                        }
                        break;
                    case ExecuteBehavior.CalculateThenDone:
                        await rule.CalculateAsync(context);
                        continue;
                }
            }
        }
    }
}
