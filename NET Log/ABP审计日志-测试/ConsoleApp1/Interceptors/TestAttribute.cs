namespace ConsoleApp1.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
        public TestAttribute()
        {

        }
    }
}
