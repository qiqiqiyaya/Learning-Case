namespace ThreadInteractive
{
	public class EmpCalcTask
	{
		private readonly MainThread _mainThread;

		public EmpCalcTask(MainThread mainThread)
		{
			_mainThread = mainThread;
		}

		public async Task ExecuteAsync(string employeeCode)
		{
			_mainThread.TaskInitReport(employeeCode);

			Random ran = new Random();
			int num = ran.Next(4000, 15000);
			await Task.Delay(num);

			_mainThread.TaskFinishReport(employeeCode);
		}
	}
}
