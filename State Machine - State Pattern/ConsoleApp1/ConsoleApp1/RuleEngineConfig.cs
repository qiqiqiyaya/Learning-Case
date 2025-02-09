namespace ConsoleApp1
{
    public class RuleEngineConfig
    {
        public List<Rule> Rules { get; set; }
    }

    public class Rule
    {
        public string Name { get; set; }
        public string Condition { get; set; }  // 条件表达式（如 "Amount >= 10000"）
        public List<RuleAction> Actions { get; set; }
    }

    public class RuleAction
    {
        public string Type { get; set; }  // 动作类型（如 JumpToState）
        public string Target { get; set; } // 目标状态
    }
}
