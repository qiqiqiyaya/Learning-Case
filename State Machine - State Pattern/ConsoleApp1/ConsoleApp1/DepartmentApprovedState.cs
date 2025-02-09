namespace ConsoleApp1
{
    public class DepartmentApprovedState : IApprovalState
    {
        public void HandleApprove(ApprovalContext context)
        {
            // 默认逻辑：进入公司审批
            context.CurrentState = new CompanyApprovedState();
            context.Result = "部门审批通过，进入公司审批阶段";

            // 触发规则引擎   
            var ruleEngine = new RuleEngine(LoadRuleConfig());
            ruleEngine.Execute(context); // 可能覆盖默认状态
        }

        public void HandleReject(ApprovalContext context)
        {
            context.CurrentState = new RejectedState();
            context.Result = "申请被部门拒绝";
        }

        public void HandleReturn(ApprovalContext context)
        {
            context.CurrentState = new SubmittedState();
            context.Result = "申请退回至提交状态";
        }

        private string LoadRuleConfig() => File.ReadAllText("Rules.json");
    }
}
