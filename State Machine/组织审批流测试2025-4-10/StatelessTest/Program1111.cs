using Stateless.Graph;
using Stateless;

namespace StatelessTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            //Bug bug = new Bug("Hello World!");

            //Console.WriteLine($"Current State: {bug.CurrentState}");

            //bug.Assign("Lamond Lu");

            //Console.WriteLine($"Current State: {bug.CurrentState}");
            //Console.WriteLine($"Current Assignee: {bug.Assignee}");

            //bug.Defer();

            //Console.WriteLine($"Current State: {bug.CurrentState}");
            //Console.WriteLine($"Current Assignee: {bug.Assignee}");

            //bug.Assign("Lu Nan");

            //Console.WriteLine($"Current State: {bug.CurrentState}");
            //Console.WriteLine($"Current Assignee: {bug.Assignee}");

            //bug.Close();

            //var info = bug.GetInfo();
            //var aa = UmlDotGraph.Format(info);

            //Console.WriteLine($"Current State: {bug.CurrentState}");

            ApprovalProcess ap = new ApprovalProcess();

            ap.SecondApprovedPass();

            ap.Submit();
            Console.WriteLine($"action: Submit");
            Console.WriteLine($"Current State: {ap.CurrentState}");

            ap.FirstApprovedPass();
            Console.WriteLine($"action: FirstApprovedPass");
            Console.WriteLine($"Current State: {ap.CurrentState}");

            ap.Reject();
            Console.WriteLine($"action: Reject");
            Console.WriteLine($"Current State: {ap.CurrentState}");

            ap.Rewrite();
            Console.WriteLine($"action: Rewrite");
            Console.WriteLine($"Current State: {ap.CurrentState}");

            ap.Submit();
            Console.WriteLine($"action: Submit");
            Console.WriteLine($"Current State: {ap.CurrentState}");

            ap.FirstApprovedPass();
            Console.WriteLine($"action: FirstApprovedPass");
            Console.WriteLine($"Current State: {ap.CurrentState}");

            ap.SecondApprovedPass();
            Console.WriteLine($"action: SecondApprovedPass");
            Console.WriteLine($"Current State: {ap.CurrentState}");
            //ap.Submit();
            //Console.WriteLine($"Current State: {ap.CurrentState}");
            //ap.Submit();
            //Console.WriteLine($"Current State: {ap.CurrentState}");

            var info = ap.GetInfo();
            var aa = UmlDotGraph.Format(info);

            Console.Read();

        }
    }
}
