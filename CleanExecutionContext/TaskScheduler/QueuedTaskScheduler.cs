using System.Collections.Concurrent;

namespace TaskScheduler
{
    public class QueuedTaskScheduler: System.Threading.Tasks.TaskScheduler, IDisposable
    {
        /// <summary>Cancellation token used for disposal.</summary>
        private readonly CancellationTokenSource _disposeCancellation = new CancellationTokenSource();

        /// <summary>The threads used by the scheduler to process work.</summary>
        private readonly Thread[] _threads;

        /// <summary>The collection of tasks to be executed on our custom threads.</summary>
        private readonly BlockingCollection<Task> _blockingTaskQueue;

        private static ThreadLocal<bool> _taskProcessingThread = new ThreadLocal<bool>();

        private readonly int _threadCount;

        public QueuedTaskScheduler(int threadCount)
        {
            _threadCount = threadCount;
            _blockingTaskQueue = new BlockingCollection<Task>();

            // create threads
            _threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                _threads[i] = new Thread(ThreadBasedDispatchLoop)
                {
                    Priority = ThreadPriority.Normal,
                    IsBackground = true,
                    Name = $"threadName ({i})"
                };
            }

            // start
            foreach (var thread in _threads)
            {
                thread.Start();
            }
        }

        /// <summary>The dispatch loop run by all threads in this scheduler.</summary>
        private void ThreadBasedDispatchLoop()
        {
            _taskProcessingThread.Value = true;

            try
            {
                // If a thread abort occurs, we'll try to reset it and continue running.
                while (true)
                {
                    try
                    {
                        // For each task queued to the scheduler, try to execute it.
                        foreach (var task in _blockingTaskQueue.GetConsumingEnumerable(_disposeCancellation.Token))
                        {
                            TryExecuteTask(task);
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        // If we received a thread abort, and that thread abort was due to shutting down
                        // or unloading, let it pass through.  Otherwise, reset the abort so we can
                        // continue processing work items.
                        if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
                        {
#pragma warning disable SYSLIB0006
                            Thread.ResetAbort();
#pragma warning restore SYSLIB0006
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // If the scheduler is disposed, the cancellation token will be set and
                // we'll receive an OperationCanceledException.  That OCE should not crash the process.
            }
            finally
            {
                _taskProcessingThread.Value = false;
            }
        }

        protected override IEnumerable<Task>? GetScheduledTasks()
        {
            return _blockingTaskQueue.ToList();
        }

        protected override void QueueTask(Task task)
        {
            // QueuedTaskScheduler 释放时，禁止向队列中添加Task
            if (_disposeCancellation.IsCancellationRequested)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            Console.WriteLine(AsyncLocalTest.Lang.Value);
            _blockingTaskQueue.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If we're already running tasks on this threads, enable inlining
            return _taskProcessingThread.Value && TryExecuteTask(task);
        }

        public void Dispose()
        {
            _disposeCancellation.Cancel();
        }
    }
}
