namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new ApprovalContext();
            context.Variables.Add("Amount", 15000);

            // 提交申请
            context.CurrentState.HandleApprove(context);

            context.CurrentState.HandleApprove(context);
            Console.WriteLine(context.CurrentState.GetType().Name); // 输出 CEOApprovalState

            Console.Read();
        }
    }
}
