namespace SimpleWorkflow.Core
{
    /// <summary>
    /// 一个步骤
    /// 自满足、可执行
    /// </summary>
    public interface IActivity
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public Task ExecuteAsync();
    }
}
