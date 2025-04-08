using Stateless;
using Stateless.Graph;
using Stateless.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatelessTest
{
    public enum PeState
    {
        /// <summary>
        /// 正在编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 第一次审批
        /// </summary>
        FirstApprove,
        /// <summary>
        /// 第二次审批
        /// </summary>
        SecondApprove,
        /// <summary>
        /// 退回
        /// </summary>
        Return,
        /// <summary>
        /// 审批完成
        /// </summary>
        Completed,
    }

    /// <summary>
    /// 触发器
    /// </summary>
    public enum PeStateTrigger
    {
        /// <summary>
        /// 提交
        /// </summary>
        Submitted,
        /// <summary>
        /// 第一次审批通过
        /// </summary>
        FirstApprovedPass,
        /// <summary>
        /// 第二次审批通过
        /// </summary>
        SecondApprovedPass,
        /// <summary>
        /// 拒绝
        /// </summary>
        Reject,
        /// <summary>
        /// 重写
        /// </summary>
        Rewrite,
    }

    public class ApprovalProcess
    {
        private PeState _state = PeState.Edit;
        private StateMachine<PeState, PeStateTrigger> _machine;
        StateMachine<PeState, PeStateTrigger>.TriggerWithParameters<string> _submittedTrigger;

        public ApprovalProcess()
        {
            _machine = new StateMachine<PeState, PeStateTrigger>(() => _state, s => _state = s);
            _submittedTrigger = _machine.SetTriggerParameters<string>(PeStateTrigger.Submitted);

            // 编辑 -> 提交
            _machine.Configure(PeState.Edit)
                .OnEntryFrom(_submittedTrigger, name =>
                {

                })
                .OnExit(() =>
                {

                })
                .Permit(PeStateTrigger.Submitted, PeState.FirstApprove);

            // 退回后，重写 -> 编辑状态
            _machine.Configure(PeState.Return)
                .Permit(PeStateTrigger.Rewrite, PeState.Edit);

            // 第一级审批
            _machine.Configure(PeState.FirstApprove)
                .Permit(PeStateTrigger.FirstApprovedPass, PeState.SecondApprove)
                .Permit(PeStateTrigger.Reject, PeState.Return);

            // 第二级审批
            _machine.Configure(PeState.SecondApprove)
                .Permit(PeStateTrigger.SecondApprovedPass, PeState.Completed)
                .Permit(PeStateTrigger.Reject, PeState.Return);
        }

        public PeState CurrentState => _state;

        public void Submit()
        {
            _machine.Fire(PeStateTrigger.Submitted);
        }

        public void Rewrite()
        {
            _machine.Fire(PeStateTrigger.Rewrite);
        }

        public void FirstApprovedPass()
        {
            _machine.Fire(PeStateTrigger.FirstApprovedPass);
        }

        public void SecondApprovedPass()
        {
            _machine.Fire(PeStateTrigger.SecondApprovedPass);
        }

        public void Reject()
        {
            _machine.Fire(PeStateTrigger.Reject);
        }

        public StateMachineInfo GetInfo()
        {
            return _machine.GetInfo();
        }
    }
}
