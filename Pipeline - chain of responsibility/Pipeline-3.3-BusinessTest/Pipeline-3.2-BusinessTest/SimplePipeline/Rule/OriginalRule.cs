namespace SimplePipeline.Rule
{
    public class OriginalRule
    {
        public int Id { get; set; }

        public string RuleType { get; set; }

        public int ParentId { get; set; }

        public int Order { get; set; }

        public string Detail { get; set; }
    }
}
