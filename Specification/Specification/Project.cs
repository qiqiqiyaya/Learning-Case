namespace Specification
{
    public class Project
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public ProjectType ProjectType { get; set; }
    }

    public enum ProjectType
    {
        A,
        B,
        C,
    }
}
