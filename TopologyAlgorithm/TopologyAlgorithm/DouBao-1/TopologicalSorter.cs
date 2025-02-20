namespace TopologyAlgorithm.DouBao_1
{
	class TopologicalSorter
	{
		// 执行拓扑排序并按顺序执行对象操作
		public static List<int> SortAndExecute(List<MyObject> objects, Dictionary<int, List<int>> dependencies)
		{
			int n = objects.Count;
			// 记录每个对象的入度（即依赖该对象的其他对象数量）
			Dictionary<int, int> inDegree = new Dictionary<int, int>();
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

			// 存储入度为 0 的对象
			Queue<int> queue = new Queue<int>();
			foreach (var obj in objects)
			{
				if (inDegree[obj.Id] == 0)
				{
					queue.Enqueue(obj.Id);
				}
			}

			List<int> result = new List<int>();
			while (queue.Count > 0)
			{
				int currentId = queue.Dequeue();
				// 找到当前对象
				MyObject currentObj = objects.Find(obj => obj.Id == currentId);
				if (currentObj != null)
				{
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

			// 如果结果列表的长度不等于对象数量，说明存在环
			if (result.Count != n)
			{
				throw new InvalidOperationException("依赖关系中存在环，无法进行拓扑排序。");
			}

			return result;
		}
	}
}
