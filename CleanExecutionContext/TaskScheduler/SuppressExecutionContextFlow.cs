namespace TaskScheduler
{
    public class SuppressExecutionContextFlow
    {
        public IDisposable CleanEnvironment()
        {
            // 阻断 ExecutionContext 流动
            ExecutionContext.SuppressFlow();
            return new DisposeAction(() =>
            {
                if (ExecutionContext.IsFlowSuppressed())
                {
                    ExecutionContext.RestoreFlow();
                }
            });
        }
    }
}
