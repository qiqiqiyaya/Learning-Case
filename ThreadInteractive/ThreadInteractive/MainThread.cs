namespace ThreadInteractive
{

	public class MainThread
	{
		private readonly SalMainTaskScheduler _taskScheduler;
		private readonly EmpCalcStatusNotice _notice;

		public MainThread(SalMainTaskScheduler taskScheduler,
			EmpCalcStatusNotice notice)
		{
			_taskScheduler = taskScheduler;
			_notice = notice;
		}

		public Task Run(Action action)
		{
			var task = new Task(action);
			task.Start(_taskScheduler);
			return task;
		}

		public Task Run(Action action, CancellationToken cancellationToken)
		{
			var task = new Task(action, cancellationToken);
			task.Start(_taskScheduler);
			return task;
		}

		public Task<TResult> Run<TResult>(Func<TResult> function)
		{
			var task = new Task<TResult>(function);
			task.Start(_taskScheduler);
			return task;
		}

		public void TaskInitReport(string empCode)
		{
			Run(() =>
			{
				Console.WriteLine(empCode + " 任务启动了...");
			});
		}

		public void TaskFinishReport(string empCode)
		{
			Report(empCode, empCode + " 任务完成了...");
		}

		/// <summary>
		/// 报告进度
		/// </summary>
		public void Report(string code, string msg)
		{
			Run(() =>
			{
				Console.WriteLine(msg);
				_notice.Remove(code);
			});
		}
	}
}
