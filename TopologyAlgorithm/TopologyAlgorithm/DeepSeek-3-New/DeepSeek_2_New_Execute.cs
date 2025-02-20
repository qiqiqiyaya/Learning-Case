namespace TopologyAlgorithm.DeepSeek_3_New
{
    internal class DeepSeek_3_New_Execute
    {
        public static void Main()
        {
            // 测试数据（包含环 1 → 2 → 3 → 1）
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2}},
                {2, new List<int> {3}},
                {3, new List<int> {1}},
                {4, new List<int> {5}},
                {5, new List<int> {6}}
            };

            var nodes = new List<int> { 1, 2, 3, 4, 5, 6 };

            try
            {
                var result = TopologicalSorter.TopologicalSort(graph, nodes);
                Console.WriteLine("拓扑排序结果: " + string.Join(" → ", result));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message); // 输出：图中存在环，环路径为：1 → 2 → 3 → 1
            }
        }
    }
}
