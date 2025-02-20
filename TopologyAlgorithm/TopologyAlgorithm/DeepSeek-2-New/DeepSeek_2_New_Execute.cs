namespace TopologyAlgorithm.DeepSeek_2_New
{
    internal class DeepSeek_2_New_Execute
    {
        public static void Main()
        {
            // 定义图的节点
            var nodes = new List<int> { 1, 2, 3, 4, 5, 6 };

            // 定义图的边（邻接表表示）
            var graph = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2, 3 } }, // 1 -> 2, 1 -> 3
                { 2, new List<int> { 4 } },    // 2 -> 4
                { 3, new List<int> { 4, 5 } }, // 3 -> 4, 3 -> 5
                { 4, new List<int> { 6,3 } },    // 4 -> 6
                { 5, new List<int> { 6 } }     // 5 -> 6
            };

            try
            {
                // 执行拓扑排序
                var sortedNodes = TopologicalSorter.TopologicalSort(graph, nodes);
                Console.WriteLine("拓扑排序结果: " + string.Join(" -> ", sortedNodes));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
