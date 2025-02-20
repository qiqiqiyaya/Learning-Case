namespace TopologyAlgorithm.DeepSeek_1
{
    public class DeepSeek_1_Execute
    {
        public static void Main()
        {
            #region 测试 case1 不通过

            //// 创建多个对象
            //var objA = new MyObject(1, "A", () =>
            //{
            //    Console.WriteLine("A 对象正在执行操作...");
            //    return 1;
            //});
            //var objB = new MyObject(2, "B", () =>
            //{
            //    Console.WriteLine("B 对象正在执行操作...");
            //    return 2;
            //});
            //var objC = new MyObject(3, "C", () =>
            //{
            //    Console.WriteLine("C 对象正在执行操作...");
            //    return 3;
            //});
            //var objD = new MyObject(4, "D", () =>
            //{
            //    Console.WriteLine("D 对象正在执行操作...");
            //    return 4;
            //});
            //var objE = new MyObject(5, "E", () =>
            //{
            //    Console.WriteLine("E 对象正在执行操作...");
            //    return 5;
            //});
            //var objF = new MyObject(6, "F", () =>
            //{
            //    Console.WriteLine("F 对象正在执行操作...");
            //    return 6;
            //});

            //var objects = new List<MyObject> { objA, objB, objC, objD, objE, objF };

            //// 定义包含环的依赖关系
            //var dependencies = new Dictionary<int, List<int>>
            //{
            //    { 1, new List<int> { 2 } }, // A 依赖 B
            //    { 2, new List<int> { 3 } }, // B 依赖 C
            //    { 3, new List<int> { 1 } }, // C 依赖 A，形成环
            //    { 4, new List<int> { 5 } }, // D 依赖 E
            //    { 5, new List<int> { 6 } }  // E 依赖 F
            //};

            //try
            //{
            //    // 执行拓扑排序并按顺序执行对象操作
            //    var results = TopologicalSorter.SortAndExecute(objects, dependencies);
            //    Console.WriteLine("最终执行结果：");
            //    foreach (var result in results)
            //    {
            //        Console.WriteLine(result);
            //    }
            //}
            //catch (InvalidOperationException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            #endregion

            #region 测试 case2

            var objA = new MyObject(1, "A", () => { Console.WriteLine("A 执行"); return 1; });
            var objB = new MyObject(2, "B", () => { Console.WriteLine("B 执行"); return 2; });
            var objC = new MyObject(3, "C", () => { Console.WriteLine("C 执行"); return 3; });
            var objD = new MyObject(4, "D", () => { Console.WriteLine("D 执行"); return 4; });
            var objE = new MyObject(5, "E", () => { Console.WriteLine("E 执行"); return 5; });
            var objF = new MyObject(6, "F", () => { Console.WriteLine("F 执行"); return 6; });

            var objects = new List<MyObject> { objA, objB, objC, objD, objE, objF };

            var dependencies = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2,3,4 } }, // A依赖B
                { 2, new List<int> { 3 } }, // B依赖C
                { 3, new List<int> { 4 } }, // C依赖A，形成环
                { 4, new List<int> { 5 } }, // D依赖E
                { 5, new List<int> { 6 } },  // E依赖F
                { 6, new List<int> {  } }  // E依赖F
        };

            try
            {
                var results = TopologicalSorter.SortAndExecute(objects, dependencies);
                Console.WriteLine("结果：" + string.Join(", ", results));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }
    }
}
