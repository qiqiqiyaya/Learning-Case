namespace ConsoleApp1
{
    // 公司审批通过状态
    public class CompanyApprovedState : IApprovalState
    {
        public void HandleApprove(ApprovalContext context)
        {
            context.CurrentState = new CompletedState();
            context.Result = "公司审批通过，流程完成";
        }

        public void HandleReject(ApprovalContext context)
        {
            context.CurrentState = new RejectedState();
            context.Result = "申请被公司拒绝";
        }

        public void HandleReturn(ApprovalContext context)
        {
            context.CurrentState = new DepartmentApprovedState();
            context.Result = "申请退回至部门审批状态";
        }
    }
}
