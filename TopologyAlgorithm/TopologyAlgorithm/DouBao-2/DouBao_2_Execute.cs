namespace TopologyAlgorithm.DouBao_2
{
    public class DouBao_2_Execute
    {
        public static void Main()
        {
            // 创建多个对象
            var objA = new MyObject(1, "A", () =>
            {
                Console.WriteLine("A 对象正在执行操作...");
                return 1;
            });
            var objB = new MyObject(2, "B", () =>
            {
                Console.WriteLine("B 对象正在执行操作...");
                return 2;
            });
            var objC = new MyObject(3, "C", () =>
            {
                Console.WriteLine("C 对象正在执行操作...");
                return 3;
            });
            var objD = new MyObject(4, "D", () =>
            {
                Console.WriteLine("D 对象正在执行操作...");
                return 4;
            });
            var objE = new MyObject(5, "E", () =>
            {
                Console.WriteLine("E 对象正在执行操作...");
                return 5;
            });
            var objF = new MyObject(6, "F", () =>
            {
                Console.WriteLine("F 对象正在执行操作...");
                return 6;
            });

            var objects = new List<MyObject> { objA, objB, objC, objD, objE, objF };

            // 定义复杂的依赖关系
            var dependencies = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2, 3 } }, // A 依赖 B 和 C
				{ 2, new List<int> { 4 } },    // B 依赖 D
				{ 3, new List<int> { 5 } },    // C 依赖 E
				{ 4, new List<int> { 6, 2 } },    // D 依赖 F
				{ 5, new List<int> { 6 } },     // E 依赖 F
			};

            try
            {
                // 执行拓扑排序并按顺序执行对象操作
                var results = TopologicalSorter.SortAndExecute(objects, dependencies);
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
