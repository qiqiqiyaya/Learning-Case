namespace SimplePipeline.Rule
{
    public class RuleConfiguration
    {
        private readonly Dictionary<string, Type> _ruleMaps;

        public RuleConfiguration()
        {
            _ruleMaps = new Dictionary<string, Type>();
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
