using Stateless;
using Stateless.Reflection;
using StatelessTest.Organization.Entities;

namespace StatelessTest.Organization.Workflows
{
    public class PeWorkflow : IWorkflow
    {
        private StateMachine<string, string> _workFlow;
        private readonly OrgDbContext _dbContext;

        public PeWorkflow(OrgDbContext dbContext)
        {
            _dbContext = dbContext;
            Definition();
        }

        /// <summary>
        /// 工作流定义
        /// </summary>
        private void Definition()
        {
            // state: Edit, FirstApprove , SecondApprove , Completed , Return
            // trigger: Submitted, FirstApprovedPass , SecondApprovedPass , Reject , Rewrite
            // 员工创建pe表单 -> 初始状态Edit状态 -> 触发提交 -> 进入第一级主管待审批状态 -> 触发审批通过 -> 进入第二级主管待审批状态 -> 触发审批通过 -> 完成状态
            // 主管待审批状态 -> 触发拒绝 -> 进入退回状态 -> 触发重写 -> 编辑状态
            State = "Edit";
            _workFlow = new StateMachine<string, string>(() => State, s => State = s);

            _workFlow.Configure("Edit")
                .Permit("Submitted", "FirstApprove");

            _workFlow.Configure("FirstApprove")
                .Permit("FirstApprovedPass", "SecondApprove")
                .Permit("Reject", "Return");

            _workFlow.Configure("SecondApprove")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            _workFlow.Configure("Return")
                .Permit("Rewrite", "Edit");
        }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public string State { get; private set; }

        public void Trigger(string eventName)
        {
            _workFlow.Fire(eventName);
        }

        public List<string> NextTrigger(string eventName)
        {
            return _workFlow.PermittedTriggers?.ToList() ?? new List<string>();
        }

        public StateMachineInfo GetInfo()
        {
            return _workFlow.GetInfo();
        }

        public async ValueTask<WorkflowEntity> CreateAsync()
        {
            var entity = new WorkflowEntity()
            {
                State = State,
                Type = WorkflowType.Pe
            };
            await _dbContext.Workflows.AddAsync(entity);
            return entity;
        }
    }
}
