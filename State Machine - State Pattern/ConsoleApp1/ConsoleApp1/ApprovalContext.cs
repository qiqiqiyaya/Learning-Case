namespace ConsoleApp1
{
    public class ApprovalContext
    {
        public IApprovalState CurrentState { get; set; }
        public string Result { get; set; }

        // 存储流程变量（用于规则条件判断）
        public Dictionary<string, int> Variables { get; set; } = new();

        public ApprovalContext()
        {
            CurrentState = new SubmittedState();
        }
    }
}
