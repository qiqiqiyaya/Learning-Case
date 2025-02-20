namespace TopologyAlgorithm.DouBao_1
{
	public class DouBao_1_Execute
	{
		public static void Main()
		{
			// 创建对象
			MyObject objC = new MyObject(3, () =>
			{
				Console.WriteLine("C 对象正在执行操作...");
				return 3;
			});
			MyObject objB = new MyObject(2, () =>
			{
				Console.WriteLine("B 对象正在执行操作...");
				return 2;
			});
			MyObject objA = new MyObject(1, () =>
			{
				Console.WriteLine("A 对象正在执行操作...");
				return 1;
			});

			List<MyObject> objects = new List<MyObject> { objA, objB, objC };

			// 定义依赖关系，键为依赖对象，值为被依赖对象列表
			Dictionary<int, List<int>> dependencies = new Dictionary<int, List<int>>
			{
				{ 1, new List<int> { 2, 3 } }, // A 依赖 B 和 C
				{ 2, new List<int> { 3 } }    // B 依赖 C
			};

			try
			{
				// 执行拓扑排序并按顺序执行对象操作
				List<int> results = TopologicalSorter.SortAndExecute(objects, dependencies);
				Console.WriteLine("最终执行结果：");
				foreach (var result in results)
				{
					Console.WriteLine(result);
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
