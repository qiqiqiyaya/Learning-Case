using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule
{
    public class RuleConfiguration
    {
        private readonly Dictionary<string, Type> _ruleMaps;

        public readonly Dictionary<int, Type> _salaryRuleRunningOrder = new Dictionary<int, Type>();

        public RuleConfiguration()
        {
            _ruleMaps = new Dictionary<string, Type>();

            _salaryRuleRunningOrder.Add(0,typeof(ICondition));
            _salaryRuleRunningOrder.Add(1, typeof(ICalculate));
            _salaryRuleRunningOrder.Add(2, typeof(ISubjectCalculate));

        }

        public void AddRule<TRule>(string key)
            where TRule : Rule
        {
            _ruleMaps.Add(key, typeof(TRule));
        }

        public Type GetRule(string key)
        {
            if (_ruleMaps.TryGetValue(key, out var rule))
            {
                return rule;
            }

            throw new Exception("ccccccccccc");
        }
    }
}
