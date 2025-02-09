namespace ConsoleApp1
{
    // 已拒绝状态
    public class RejectedState : IApprovalState
    {
        public void HandleApprove(ApprovalContext context)
        {
            context.Result = "错误：申请已被拒绝，无法重新审批";
        }

        public void HandleReject(ApprovalContext context)
        {
            context.Result = "错误：申请已被拒绝";
        }

        public void HandleReturn(ApprovalContext context)
        {
            context.Result = "错误：申请已被拒绝，无法退回";
        }
    }
}
