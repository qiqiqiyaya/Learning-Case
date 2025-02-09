using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq.Dynamic.Core; // 动态表达式解析库
using System.Linq.Expressions;
using System.Text.Json;

namespace ConsoleApp1
{

    public class RuleEngine
    {
        private readonly List<Rule> _rules;

        public RuleEngine(string ruleConfigJson)
        {
            // 加载规则配置
            var config = JsonSerializer.Deserialize<RuleEngineConfig>(ruleConfigJson);
            _rules = config.Rules;
        }

        // 执行规则匹配
        public void Execute(ApprovalContext context)
        {
            foreach (var rule in _rules)
            {
                // 动态解析条件表达式
                var isMatch = EvaluateCondition(context.Variables, rule.Condition);
                if (!isMatch) continue;

                // 执行匹配规则的动作
                foreach (var action in rule.Actions)
                {
                    HandleAction(context, action);
                }
                break; // 匹配第一个符合条件的规则
            }
        }

        // 解析条件表达式
        private bool EvaluateCondition(Dictionary<string, int> variables, string condition)
        {
            try
            {
                // 使用 Dynamic LINQ 解析表达式
                var parameter = Expression.Parameter(typeof(Dictionary<string, int>), "vars");
                var expr = DynamicExpressionParser.ParseLambda(
                    new[] { parameter },
                    typeof(bool),
                    condition
                );

                // 编译并执行表达式
                var func = (Func<Dictionary<string, int>, bool>)expr.Compile();
                return func(variables);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // 处理动作
        private void HandleAction(ApprovalContext context, RuleAction action)
        {
            switch (action.Type)
            {
                case "JumpToState":
                    // 根据目标状态名称反射创建状态对象
                    var stateType = Type.GetType($"ConsoleApp1.{action.Target}");
                    if (stateType != null && typeof(IApprovalState).IsAssignableFrom(stateType))
                    {
                        context.CurrentState = (IApprovalState)Activator.CreateInstance(stateType);
                        context.Result = $"规则触发：跳转到 {action.Target}";
                    }
                    break;
            }
        }
    }
}
