namespace ExtractionData
{
    /// <summary>
    /// 提供异步暂停操作
    /// </summary>
    /// <remarks>
    /// 源代码请参考 https://stackoverflow.com/questions/19613444/a-pattern-to-pause-resume-an-async-task
    /// </remarks>
    public class PauseTokenSource : IDisposable
    {
        private readonly object _lock = new Object();
        bool _paused = false; // could use resumeRequest as flag too

        internal static readonly Task CompletedTask = Task.FromResult(true);
        /// <summary>
        /// 暂停响应
        /// </summary>
        private TaskCompletionSource<bool>? _pauseResponse;
        /// <summary>
        /// 重用请求
        /// </summary>
        private TaskCompletionSource<bool>? _resumeRequest;

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            lock (_lock)
            {
                // 已暂停，直接返回
                if (_paused)
                    return;
                _paused = true;
                _pauseResponse = null;
                _resumeRequest = new TaskCompletionSource<bool>();
            }
        }

        public void Resume()
        {
            TaskCompletionSource<bool>? resumeRequest = null;

            lock (_lock)
            {
                if (!_paused)
                    return;
                _paused = false;
                resumeRequest = _resumeRequest;
                _resumeRequest = null;
            }

            resumeRequest?.TrySetResult(true);
        }

        // pause with feedback
        // that the producer task has reached the paused state
        public Task PauseAsync()
        {
            Task? responseTask = null;

            lock (_lock)
            {
                if (_paused)
                    return _pauseResponse?.Task!;
                _paused = true;
                _pauseResponse = new TaskCompletionSource<bool>();
                _resumeRequest = new TaskCompletionSource<bool>();
                responseTask = _pauseResponse.Task;
            }

            return responseTask;
        }

        public Task WaitAsync()
        {
            Task? resumeTask = null;
            TaskCompletionSource<bool>? response = null;

            lock (_lock)
            {
                if (!_paused)
                    return CompletedTask;
                response = _pauseResponse;
                resumeTask = _resumeRequest?.Task!;
            }

            response?.TrySetResult(true);
            return resumeTask;
        }

        public bool IsPaused
        {
            get
            {
                lock (_lock)
                    return _paused;
            }
        }

        public PauseToken Token => new PauseToken(this);

        public void Dispose()
        {
            _pauseResponse?.SetException(new OperationCanceledException());
            _resumeRequest?.SetException(new OperationCanceledException());
        }
    }
}
