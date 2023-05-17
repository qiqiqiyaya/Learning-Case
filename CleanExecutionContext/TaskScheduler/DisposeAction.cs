using System.Diagnostics.CodeAnalysis;

namespace TaskScheduler
{
    /// <summary>
    /// 源代码来自ABP Vnext框架
    /// </summary>
    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        public DisposeAction([NotNull] Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Dispose()
        {
            _action();
        }
    }
}
