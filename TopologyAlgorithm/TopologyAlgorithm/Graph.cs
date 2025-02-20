namespace TopologyAlgorithm
{
	class Graph
	{
		private int V; // 顶点数
		private List<int>[] adj; // 邻接表

		public Graph(int v)
		{
			V = v;
			adj = new List<int>[V];
			for (int i = 0; i < V; i++)
			{
				adj[i] = new List<int>();
			}
		}

		// 添加边
		public void AddEdge(int v, int w)
		{
			adj[v].Add(w);
			adj[w].Add(v); // 如果是无向图
		}

		// 打印图
		public void PrintGraph()
		{
			for (int i = 0; i < V; i++)
			{
				Console.Write("顶点 " + i + " 的邻接顶点: ");
				foreach (var vertex in adj[i])
				{
					Console.Write(vertex + " ");
				}
				Console.WriteLine();
			}
		}
	}

	class Program
	{
		static void Main()
		{
			Graph g = new Graph(4);
			g.AddEdge(0, 1);
			g.AddEdge(0, 2);
			g.AddEdge(1, 2);
			g.AddEdge(2, 3);

			g.PrintGraph();
		}
	}
}
