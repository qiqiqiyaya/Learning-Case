namespace TopologyAlgorithm.DeepSeek_2_New
{
    internal class TopologicalSorter
    {
        public static List<int> TopologicalSort(Dictionary<int, List<int>> graph, List<int> nodes)
        {
            // 存储每个节点的入度
            var inDegree = new Dictionary<int, int>();
            foreach (var node in nodes)
            {
                inDegree[node] = 0;
            }

            // 计算每个节点的入度
            foreach (var node in nodes)
            {
                if (graph.ContainsKey(node))
                {
                    foreach (var neighbor in graph[node])
                    {
                        inDegree[neighbor]++;
                    }
                }
            }

            // 将入度为 0 的节点加入队列
            var queue = new Queue<int>();
            foreach (var node in nodes)
            {
                if (inDegree[node] == 0)
                {
                    queue.Enqueue(node);
                }
            }

            // 存储拓扑排序的结果
            var result = new List<int>();

            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();
                result.Add(currentNode);

                // 减少当前节点的邻居节点的入度
                if (graph.ContainsKey(currentNode))
                {
                    foreach (var neighbor in graph[currentNode])
                    {
                        inDegree[neighbor]--;
                        if (inDegree[neighbor] == 0)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            // 如果结果的数量不等于节点数量，说明存在环
            if (result.Count != nodes.Count)
            {
                throw new InvalidOperationException("图中存在环，无法进行拓扑排序！");
            }

            return result;
        }
    }
}
