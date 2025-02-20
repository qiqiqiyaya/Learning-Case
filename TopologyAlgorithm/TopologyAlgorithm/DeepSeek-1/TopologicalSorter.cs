namespace TopologyAlgorithm.DeepSeek_1
{
    class TopologicalSorter
    {
        public static List<int> SortAndExecute(List<MyObject> objects, Dictionary<int, List<int>> dependencies)
        {
            // 构建后继列表和初始化入度
            var successors = new Dictionary<int, List<int>>();
            var inDegree = new Dictionary<int, int>();

            foreach (var obj in objects)
            {
                inDegree[obj.Id] = 0;
                successors[obj.Id] = new List<int>();
            }

            foreach (var dep in dependencies)
            {
                int node = dep.Key;
                foreach (var dependency in dep.Value)
                {
                    successors[dependency].Add(node);
                    inDegree[node]++;
                }
            }

            var queue = new Queue<int>();
            foreach (var obj in objects)
            {
                if (inDegree[obj.Id] == 0)
                {
                    queue.Enqueue(obj.Id);
                }
            }

            var result = new List<int>();
            var sortedOrder = new List<int>();
            var parentMap = new Dictionary<int, int>();

            while (queue.Count > 0)
            {
                int currentId = queue.Dequeue();
                sortedOrder.Add(currentId);

                MyObject currentObj = objects.Find(obj => obj.Id == currentId);
                if (currentObj != null)
                {
                    Console.WriteLine($"Executing {currentObj}");
                    int output = currentObj.Action();
                    result.Add(output);

                    foreach (var successor in successors[currentId])
                    {
                        parentMap[successor] = currentId;
                        inDegree[successor]--;
                        if (inDegree[successor] == 0)
                        {
                            queue.Enqueue(successor);
                        }
                    }
                }
            }

            if (sortedOrder.Count != objects.Count)
            {
                var remainingNodes = objects.Select(obj => obj.Id).Except(sortedOrder).ToList();
                var cycle = FindCycle(remainingNodes, dependencies);
                var cycleInfo = string.Join(" -> ", cycle);
                throw new InvalidOperationException($"依赖关系中存在环，环的节点顺序为: {cycleInfo}");
            }

            return result;
        }

        private static List<int> FindCycle(List<int> remainingNodes, Dictionary<int, List<int>> dependencies)
        {
            var visited = new Dictionary<int, VisitState>();
            var path = new List<int>();

            foreach (var node in remainingNodes)
            {
                if (DFS(node, dependencies, visited, path))
                {
                    // 提取环
                    int cycleStart = path.LastIndexOf(path[path.Count - 1]);
                    return path.Skip(cycleStart).ToList();
                }
            }

            return new List<int>(); // 未找到环，理论上不会发生
        }

        private enum VisitState
        {
            NotVisited,
            Visiting,
            Visited
        };

        private static bool DFS(int node, Dictionary<int, List<int>> dependencies, Dictionary<int, VisitState> visited, List<int> path)
        {
            if (!visited.ContainsKey(node))
                visited[node] = VisitState.NotVisited;

            if (visited[node] == VisitState.Visiting)
            {
                path.Add(node);
                return true;
            }

            if (visited[node] == VisitState.Visited)
                return false;

            visited[node] = VisitState.Visiting;
            path.Add(node);

            if (dependencies.ContainsKey(node))
            {
                foreach (var neighbor in dependencies[node])
                {
                    if (DFS(neighbor, dependencies, visited, path))
                        return true;
                }
            }

            visited[node] = VisitState.Visited;
            path.RemoveAt(path.Count - 1);
            return false;
        }
    }

}
