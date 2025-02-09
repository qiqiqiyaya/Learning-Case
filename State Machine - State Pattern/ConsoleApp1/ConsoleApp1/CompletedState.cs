using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // 已完成状态
    public class CompletedState : IApprovalState
    {
        public void HandleApprove(ApprovalContext context)
        {
            context.Result = "流程已完成，无需重复审批";
        }

        public void HandleReject(ApprovalContext context)
        {
            context.Result = "错误：流程已完成，无法拒绝";
        }

        public void HandleReturn(ApprovalContext context)
        {
            context.Result = "错误：流程已完成，无法退回";
        }
    }
}
