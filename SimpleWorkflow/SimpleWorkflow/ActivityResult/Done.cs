namespace SimpleWorkflow.ActivityResult
{
    public class Done : IActivityResult
    {
        public string GetValue()
        {
            return Const.Done;
        }

        public static IActivityResult Result => new Done();
    }
}
