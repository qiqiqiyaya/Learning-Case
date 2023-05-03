namespace SimplePipeline.Scope
{
    public abstract class Scope
    {
        public int Id { get; set; }

        public abstract bool IsSatisfy(Employee employee);
    }
}
