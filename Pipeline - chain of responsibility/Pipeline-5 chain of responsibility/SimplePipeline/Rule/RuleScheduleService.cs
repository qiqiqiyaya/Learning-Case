using SimplePipeline.Rule.Chain;
using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule
{
    public class RuleScheduleService : IRuleScheduleService
    {
        private readonly IRuleResolveService _resolveService;

        public RuleScheduleService(IRuleResolveService resolveService)
        {
            _resolveService = resolveService;
        }

        public async Task CalculateAsync(List<Rule> rules, RuleCalculationContext context)
        {
            FilterPipeline pipeline = new FilterPipeline(new FilterPipelineConfiguration());
            context.AddRules(rules);
            foreach (var rule in rules)
            {
                pipeline.AddFilter(rule);
            }

            await pipeline.RunAsync(context);
        }

        public async Task CalculateAsync(RuleCalculationContext context)
        {
            var rules = _resolveService.ConvertTree(context.Rules);

            FilterPipeline pipeline = new FilterPipeline(new FilterPipelineConfiguration());
            context.AddRules(rules);
            foreach (var rule in rules)
            {
                pipeline.AddFilter(rule);
            }

            await pipeline.RunAsync(context);




            //foreach (var tree in trees)
            //{
                //if (IsTopOfNode(tree))
                //{
                //    // TODO 记录当前rule
                //}

                //await ChildrenCalculateAsync(tree.Children, context);

                //var tresse = trees.Where(x => x.Order > tree.Order).ToList();

                // 1 rule 是否是条件
                //  1.1 查其他rule 是否与此rule 类型相同 
                //  1.2 类型相同 or
                //  1.3 类型不同 and
                //  1.4 从scope过来只有 or

                // 子级
                // 1 rule 是否是条件


                // 总结 同级rule中 ，只有 条件的rule ，才会做 and or关系


                // 1. 是否相同的rule 
                // 2. 是否从scope来的
                // 3. 

                // 第一级节点 啥都不需要做，只记录下
                // 1. rule为条件，继承 ICondition ，判断是否满足此规则
                //  1.2 满足rule条件，向下或者向子级执行
                // 2. rule不为条件，记录rule，继续向下或子级执行
                // 3. 如果有IRuleCalculate，则计算得到金额，结束
                // 4. 所有rule执行完成

                // or   and  关系只会存在同一级
                // or 同级规则一样，且 rule 为条件 ，如果rule不为条件 ， 只需记录rule，不需要计算
                // and 同级规则不一样
            //}
        }

        public async Task ChildrenCalculateAsync(List<Rule> rules, RuleCalculationContext context)
        {
            // 判断 是否是 同类型规定，是则 or ，否则 and
            // 1. 先判断是否是 条件
            if (AllCondition(rules))
            {
                // 2. 再判断 逻辑关系
                var re = Relationship(rules);

                if (re == LogicalRelationship.And)
                {
                    // 所有条件都要满足
                    foreach (var rule in rules)
                    {
                        var condition = rule as ICondition;
                        if (await condition.IsSatisfy(context))
                        {

                        }
                    }
                }

                if (re == LogicalRelationship.Or)
                {
                    // 满足一个条件即可
                    foreach (var rule in rules)
                    {
                        var condition = rule as ICondition;
                        if (await condition.IsSatisfy(context))
                        {

                            if (rule is ICalculate calculate)
                            {
                                await calculate.CalculateAsync(context);
                                // 结束
                                return;
                            }
                            else if (rule.HasChildren)
                            {
                                await ChildrenCalculateAsync(rule.Children, context);
                            }
                        }
                    }
                }
            }


        }

        public bool IsTopOfNode(Rule tree)
        {
            return !tree.ParentId.HasValue;
        }

        public LogicalRelationship Relationship(List<Rule> rules)
        {
            // 判断
            return LogicalRelationship.And;
        }
        public bool AllCondition(List<Rule> rules)
        {
            return rules.All(s => s is ICondition);
        }
    }

    public enum LogicalRelationship
    {
        Or,
        And
    }
}
