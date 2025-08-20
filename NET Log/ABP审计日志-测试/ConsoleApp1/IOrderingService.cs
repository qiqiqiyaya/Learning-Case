namespace ConsoleApp1
{
	internal interface IOrderingService
	{
		Task<int> Get(string id);
	}
}
