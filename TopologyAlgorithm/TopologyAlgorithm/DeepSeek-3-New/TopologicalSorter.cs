namespace TopologyAlgorithm.DeepSeek_3_New
{
    class TopologicalSorter
    {
        public static List<int> TopologicalSort(Dictionary<int, List<int>> graph, List<int> nodes)
        {
            var inDegree = new Dictionary<int, int>();
            foreach (var node in nodes)
                inDegree[node] = 0;

            // 构建反向依赖关系（邻接表）
            var reverseDependencies = new Dictionary<int, List<int>>();
            foreach (var node in nodes)
                reverseDependencies[node] = new List<int>();

            foreach (var entry in graph)
            {
                foreach (var dependent in entry.Value)
                {
                    inDegree[dependent]++;
                    reverseDependencies[dependent].Add(entry.Key);
                }
            }

            var queue = new Queue<int>();
            foreach (var node in nodes)
                if (inDegree[node] == 0) queue.Enqueue(node);

            var sorted = new List<int>();
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                sorted.Add(node);

                if (graph.ContainsKey(node))
                {
                    foreach (var neighbor in graph[node])
                    {
                        if (--inDegree[neighbor] == 0)
                            queue.Enqueue(neighbor);
                    }
                }
            }

            // 检测到环时查找具体路径
            if (sorted.Count != nodes.Count)
            {
                var cycle = FindCycle(nodes.Except(sorted).ToList(), reverseDependencies);
                throw new InvalidOperationException($"图中存在环，环路径为：{string.Join(" → ", cycle)}");
            }

            return sorted;
        }

        private static List<int> FindCycle(List<int> remainingNodes, Dictionary<int, List<int>> reverseGraph)
        {
            var visited = new Dictionary<int, int>(); // 记录访问状态和父节点
            var stack = new Stack<int>();
            var cycle = new List<int>();

            foreach (var startNode in remainingNodes)
            {
                if (visited.ContainsKey(startNode)) continue;

                stack.Push(startNode);
                visited[startNode] = -1; // 起始节点的父节点设为-1

                while (stack.Count > 0)
                {
                    var current = stack.Peek();
                    bool foundUnvisited = false;

                    foreach (var parent in reverseGraph[current])
                    {
                        if (!visited.ContainsKey(parent))
                        {
                            visited[parent] = current;
                            stack.Push(parent);
                            foundUnvisited = true;
                            break;
                        }
                        else if (stack.Contains(parent)) // 发现环
                        {
                            // 提取环路径
                            cycle.Add(parent);
                            while (stack.Peek() != parent)
                                cycle.Add(stack.Pop());
                            cycle.Reverse();
                            return cycle;
                        }
                    }

                    if (!foundUnvisited) stack.Pop();
                }
            }
            return cycle;
        }
    }
}
