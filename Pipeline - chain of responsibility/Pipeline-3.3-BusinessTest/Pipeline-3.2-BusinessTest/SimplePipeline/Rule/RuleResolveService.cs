using Newtonsoft.Json;
using SimplePipeline.Rule.Rules;

namespace SimplePipeline.Rule
{
    public class RuleResolveService : IRuleResolveService
    {
        private readonly RuleConfiguration _configuration;

        public RuleResolveService(RuleConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Rule> GetRules(List<OriginalRule> originalRules)
        {
            return originalRules.Select(GetRule).ToList();
        }

        public Rule GetRule(OriginalRule originalRule)
        {
            var type = _configuration.GetRule(originalRule.RuleType);

            var obj = JsonConvert.DeserializeObject(originalRule.Detail, type);
            var rule= obj as Rule;
            if (rule == null)
            {
                throw new Exception("");
            }

            return rule;
        }

        public T GetRule<T>(OriginalRule originalRule)
        {
            T? rule = JsonConvert.DeserializeObject<T>(originalRule.Detail);
            if (rule == null)
            {
                throw new Exception("");
            }

            return rule;
        }

        public List<Rule> ConvertTree(List<Rule> rules)
        {
            return rules;
        }

        public List<Rule> ConvertTree(List<OriginalRule> originalRules)
        {
            var rules = GetRules(originalRules);
            return rules;
        }

        public void GetJson()
        {
            var dic = new Dictionary<string, Type>();

            dic.Add("", typeof(RegionRule));
        }
    }
}