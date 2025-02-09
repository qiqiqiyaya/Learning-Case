namespace ConsoleApp1
{
    // 审批状态接口
    public interface IApprovalState
    {
        void HandleApprove(ApprovalContext context);
        void HandleReject(ApprovalContext context);
        void HandleReturn(ApprovalContext context);
    }
}
