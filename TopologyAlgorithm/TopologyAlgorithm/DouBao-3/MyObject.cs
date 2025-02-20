namespace TopologyAlgorithm.DouBao_3
{
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
}
