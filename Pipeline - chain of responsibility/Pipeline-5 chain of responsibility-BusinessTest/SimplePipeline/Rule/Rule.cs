using Autofac.Core;
using Newtonsoft.Json;
using SimplePipeline.Rule.Chain;
using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule
{
    public abstract class Rule : RuleFilterChain
    {
        public int Id { get; set; }

        public string RuleType { get; set; }

        public int? ParentId { get; set; }

        public int Order { get; set; }

        public List<Rule> Children { get; set; }

        [JsonIgnore]
        public bool HasChildren => Children.Count > 0;

        /// <summary>
        /// 是否满足此规则
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task<bool> IsSatisfy(RuleCalculationContext context)
        {
            return Task.FromResult(true);
        }

        protected override Task Next(RuleCalculationContext context)
        {
            context.AdjustNextRules(this);
            return base.Next(context);
        }

        /// <summary>
        /// 获取当前 <see cref="Rule"/> 的逻辑关系
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual RuleLogicalRelation GetRelationship(RuleCalculationContext context)
        {
            // 只有条件 Rule 才会执行此方法
            if (!GetType().GetInterfaces().Contains(typeof(ICondition)))
            {
                throw new Exception("The current rule is not ICondition.");
            }

            // 剩余所有的Rules中有相同的 rule 则为Or，否则为 and
            if (context.CurrentPipelineNextRules.Any(x => x.GetType() == this.GetType()))
            {
                return RuleLogicalRelation.Or;
            }

            return RuleLogicalRelation.And;
        }

        /// <summary>
        /// 是否需要计算
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task<bool> NeedCalculateAsync(RuleCalculationContext context)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// 执行逻辑处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task RelationshipHandle(RuleCalculationContext context)
        {
            var relation = GetRelationship(context);
            var satisfy = await IsSatisfy(context);

            // 1. 所有Rule都有IsSatisfy，默认值返回true，如果是 条件Rule ，可以重写自己的判断逻辑
            // 2. 判断当前 rule
            if (satisfy)
            {
                // 1.是否需要计算，需要则计算，得到 “值” 再返回
                // 2.不需要计算，则执行 子级 。
                // 3.则执行子级，没有子级，则同级向下执行

                var need = await NeedCalculateAsync(context);
                if (need)
                {
                    var cal = this as ICalculate;
                    if (cal == null) throw new Exception("The current rule is not ICalculate");
                    await cal.CalculateAsync(context);
                    return;
                }

                if (!need && this.HasChildren)
                {
                    var scheduleService = context.GetRequiredService<IRuleScheduleService>();
                    await scheduleService.CalculateAsync(this.Children, context);

                    // 当前 Rule 为 or ，则无需执行同级 Rule
                    if (relation == RuleLogicalRelation.Or)
                    {
                        return;
                    }
                    else
                    {
                        // 执行同级
                        await Next(context);
                        return;
                    }
                }

                // 当前 Rule 为 or ，则无需执行同级 Rule
                if (relation == RuleLogicalRelation.Or)
                {
                    return;
                }
                else
                {
                    // 执行同级
                    await Next(context);
                    return;
                }
            }

            if (!satisfy && relation == RuleLogicalRelation.And)
            {
                // “值” 取 0 ，直接返回
                // TODO context中设置结果，此科目中所有rule执行结束
                return;
            }

            if (!satisfy && relation == RuleLogicalRelation.Or)
            {
                // 同级向下执行，不执行子级
                await Next(context);
                return;
            }
        }
    }
}
