using Bogus.DataSets;
using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public class RuleCalculationContext
    {
        public RuleCalculationContext(HandlerExecutionContext context)
        {
            Context = context;
        }

        public List<OriginalRule> Rules => Context.Subject.Rules;

        public Employee Employee => Context.Employee;

        protected HandlerExecutionContext Context { get; }

        /// <summary>
        /// 当前管道的余下的Rules
        /// </summary>
        public List<Rule> CurrentPipelineNextRules { get; private set; }

        /// <summary>
        /// 当前管道的所有Rules
        /// </summary>
        public List<Rule> CurrentPipelineAllRules { get; private set; }

        /// <summary>
        /// 添加rules集合
        /// </summary>
        /// <param name="rules"></param>
        public void AddRules(List<Rule> rules)
        {
            CurrentPipelineAllRules = rules;
            CurrentPipelineNextRules = rules;
        }

        /// <summary>
        /// 设置剩余的rules
        /// </summary>
        /// <param name="previousRule"></param>
        public void AdjustNextRules(Rule previousRule)
        {
            CurrentPipelineNextRules.Remove(previousRule);
        }

        /// <summary>
        /// 获取同级下一个rule
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Rule? GetNextRule(Rule current)
        {
            var index = CurrentPipelineNextRules.IndexOf(current);
            if (index < 0) throw new Exception("Can't get index of rule");

            // 如果最后一个rule，next rule 放回 null
            if (index == CurrentPipelineNextRules.Count) return null;
            return CurrentPipelineNextRules[++index];
        }

        public T GetRequiredService<T>()
            where T : notnull
        {
            return Context.GetRequiredService<T>();
        }

        public virtual void AddCalculateResult(RuleCalculateResult result)
        {
            Context.Result.CalculateResults.Add(result);
        }
    }
}
