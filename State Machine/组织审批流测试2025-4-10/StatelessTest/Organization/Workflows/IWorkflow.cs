using Stateless.Reflection;

namespace StatelessTest.Organization.Workflows
{
    public interface IWorkflow
    {
        /// <summary>
        /// 工作流状态
        /// </summary>
        public string State { get; }

        public void Trigger(string eventName);

        public StateMachineInfo GetInfo();
    }
}
