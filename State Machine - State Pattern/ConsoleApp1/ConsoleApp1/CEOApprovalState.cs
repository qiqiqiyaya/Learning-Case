namespace ConsoleApp1
{
    public class CEOApprovalState : IApprovalState
    {
        public void HandleApprove(ApprovalContext context)
        {
            context.CurrentState = new CompletedState();
            context.Result = "CEO审批通过，流程完成";
        }

        public void HandleReject(ApprovalContext context)
        {
            throw new NotImplementedException();
        }

        public void HandleReturn(ApprovalContext context)
        {
            throw new NotImplementedException();
        }
    }
}
