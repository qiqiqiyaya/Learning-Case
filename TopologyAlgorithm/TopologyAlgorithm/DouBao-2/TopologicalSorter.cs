namespace TopologyAlgorithm.DouBao_2
{
    class TopologicalSorter
    {
        public static List<int> SortAndExecute(List<MyObject> objects, Dictionary<int, List<int>> dependencies)
        {
            // 存储每个对象的入度
            var inDegree = new Dictionary<int, int>();
            foreach (var obj in objects)
            {
                inDegree[obj.Id] = 0;
            }

            // 计算每个对象的入度
            foreach (var dep in dependencies)
            {
                foreach (var dependentId in dep.Value)
                {
                    inDegree[dependentId]++;
                }
            }

            // 存储入度为 0 的对象的队列
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

            while (queue.Count > 0)
            {
                int currentId = queue.Dequeue();
                sortedOrder.Add(currentId);

                // 找到当前对象
                MyObject currentObj = objects.Find(obj => obj.Id == currentId)!;
                if (currentObj != null)
                {
                    Console.WriteLine($"Executing {currentObj}");
                    // 执行当前对象的操作
                    int output = currentObj.Action();
                    result.Add(output);

                    // 检查当前对象的依赖对象
                    if (dependencies.ContainsKey(currentId))
                    {
                        foreach (var dependentId in dependencies[currentId])
                        {
                            // 减少依赖对象的入度
                            inDegree[dependentId]--;
                            if (inDegree[dependentId] == 0)
                            {
                                queue.Enqueue(dependentId);
                            }
                        }
                    }
                }
            }

            // 检查是否存在环
            if (sortedOrder.Count != objects.Count)
            {
                throw new InvalidOperationException("依赖关系中存在环，无法进行拓扑排序。");
            }

            return result;
        }
    }

}
