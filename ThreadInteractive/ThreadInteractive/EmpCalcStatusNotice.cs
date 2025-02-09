namespace ThreadInteractive
{
	public class EmpCalcStatusNotice
	{
		private TaskCompletionSource? _taskCompletionSource;
		private readonly HashSet<string> _container = new HashSet<string>();

		public void Start()
		{
			_taskCompletionSource = new TaskCompletionSource();
		}

		public void AddId(string empCode)
		{
			_container.Add(empCode);
		}

		public void Remove(string empCode)
		{
			_container.Remove(empCode);
			if (_container.Count == 0)
			{
				End();
			}
		}

		public void End()
		{
			if (_taskCompletionSource == null)
			{
				throw new Exception("EmpCalcStatusNotice didn't started");
			}

			_taskCompletionSource.SetResult();
		}

		public Task Task
		{
			get
			{
				if (_taskCompletionSource == null)
				{
					throw new Exception("EmpCalcStatusNotice didn't started");
				}

				return _taskCompletionSource.Task;
			}
		}
	}
}
