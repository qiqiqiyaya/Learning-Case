namespace TopologyAlgorithm
{
	class Node
	{
		public int Data;
		public Node Left, Right;

		public Node(int item)
		{
			Data = item;
			Left = Right = null;
		}
	}

	class BinaryTree
	{
		public Node Root;

		public BinaryTree()
		{
			Root = null;
		}

		// 中序遍历
		public void InOrderTraversal(Node node)
		{
			if (node == null)
				return;

			InOrderTraversal(node.Left);
			Console.Write(node.Data + " ");
			InOrderTraversal(node.Right);
		}
	}
}
