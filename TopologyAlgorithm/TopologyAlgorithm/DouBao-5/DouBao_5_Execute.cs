namespace TopologyAlgorithm.DouBao_5
{
    using System;
    using System.Collections.Generic;

    // 定义对象类
    class MyObject
    {
        public int Id { get; }
        public string Name { get; }
        public Func<int> Action { get; }

        public MyObject(int id, string name, Func<int> action)
        {
            Id = id;
            Name = name;
            Action = action;
        }

        public override string ToString()
        {
            return $"Object {Name} (ID: {Id})";
        }
    }

    class TopologicalSorter
    {
        // 用于标记节点的状态
        enum NodeState
        {
            Unvisited,
            Visiting,
            Visited
        }

        public static List<int> SortAndExecute(List<MyObject> objects, Dictionary<int, List<int>> dependencies)
        {
            var graph = new Dictionary<int, List<int>>();
            foreach (var obj in objects)
            {
                graph[obj.Id] = new List<int>();
            }
            foreach (var dep in dependencies)
            {
                graph[dep.Key] = dep.Value;
            }

            var states = new Dictionary<int, NodeState>();
            foreach (var obj in objects)
            {
                states[obj.Id] = NodeState.Unvisited;
            }

            var result = new List<int>();
            var stack = new Stack<int>();
            var cycle = new List<int>();

            foreach (var obj in objects)
            {
                if (states[obj.Id] == NodeState.Unvisited)
                {
                    if (!DFS(obj.Id, graph, states, stack, ref cycle))
                    {
                        var cycleInfo = string.Join(" -> ", cycle);
                        throw new InvalidOperationException($"依赖关系中存在环，环的节点顺序为: {cycleInfo}");
                    }
                }
            }

            while (stack.Count > 0)
            {
                int id = stack.Pop();
                MyObject obj = objects.Find(o => o.Id == id);
                result.Add(obj.Action());
            }

            return result;
        }

        private static bool DFS(int node, Dictionary<int, List<int>> graph, Dictionary<int, NodeState> states, Stack<int> stack, ref List<int> cycle)
        {
            states[node] = NodeState.Visiting;
            cycle.Add(node);

            if (graph.ContainsKey(node))
            {
                foreach (var neighbor in graph[node])
                {
                    if (states[neighbor] == NodeState.Visiting)
                    {
                        // 发现环，回溯找出环的具体信息
                        int startIndex = cycle.IndexOf(neighbor);
                        if (startIndex != -1)
                        {
                            cycle = cycle.GetRange(startIndex, cycle.Count - startIndex);
                        }

                        // 形成环的最后一个节点
                        cycle.Add(neighbor);
                        return false;
                    }
                    if (states[neighbor] == NodeState.Unvisited)
                    {
                        if (!DFS(neighbor, graph, states, stack, ref cycle))
                        {
                            return false;
                        }
                    }
                }
            }

            states[node] = NodeState.Visited;
            stack.Push(node);
            cycle.RemoveAt(cycle.Count - 1);
            return true;
        }
    }

    public class DouBao_5_Execute
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

            // 定义包含环的依赖关系
            var dependencies = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2 } }, // A 依赖 B
                { 2, new List<int> { 3 } }, // B 依赖 C
                { 3, new List<int> { 1 } }, // C 依赖 A，形成环
                { 4, new List<int> { 5 } }, // D 依赖 E
                { 5, new List<int> { 6 } } // E 依赖 F
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
