using Newtonsoft.Json;
using SimplePipeline.Scope;

namespace SimplePipeline.Rule
{
    public abstract class Rule
    {
        public int Id { get; set; }

        public string RuleType { get; set; }

        public int ParentId { get; set; }

        public int Order { get; set; }

        public List<Rule> Children { get; set; }

        [JsonIgnore]
        public bool HasChildren => Children.Count > 0;

        [JsonIgnore]
        public virtual ExecuteBehavior Behavior => ExecuteBehavior.CalculateThenDone;

        public abstract Task<bool> IsSatisfy(Employee employee, RuleCalculationContext context);

        public abstract Task<RuleCalculateResult> CalculateAsync(RuleCalculationContext context);

        public T Convert<T>()
            where T : Rule
        {
            var rule = this as T;
            if (rule == null)
            {
                throw new Exception($"Can't convert to {typeof(T).Name}");
            }

            return rule;
        }
    }
}
