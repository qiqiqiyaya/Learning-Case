using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologyAlgorithm.DouBao_1
{
	// 定义对象类
	class MyObject
	{
		public int Id { get; }
		public Func<int> Action { get; }

		public MyObject(int id, Func<int> action)
		{
			Id = id;
			Action = action;
		}
	}
}
