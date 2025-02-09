namespace ConsoleApp1
{
    // 已提交状态
    public class SubmittedState : IApprovalState
    {
        public void HandleApprove(ApprovalContext context)
        {
            context.CurrentState = new DepartmentApprovedState();
            context.Result = "申请已提交，进入部门审批阶段";
        }

        public void HandleReject(ApprovalContext context)
        {
            context.CurrentState = new RejectedState();
            context.Result = "申请被拒绝";
        }

        public void HandleReturn(ApprovalContext context)
        {
            context.Result = "错误：申请尚未提交，无法退回";
        }
    }
}
