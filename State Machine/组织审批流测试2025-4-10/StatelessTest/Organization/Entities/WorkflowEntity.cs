namespace StatelessTest.Organization.Entities
{
    public class WorkflowEntity
    {
        public int Id { get; set; }

        public WorkflowType Type { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        public string State { get; set; }
    }

    public enum WorkflowType
    {
        Pe = 1
    }
}
