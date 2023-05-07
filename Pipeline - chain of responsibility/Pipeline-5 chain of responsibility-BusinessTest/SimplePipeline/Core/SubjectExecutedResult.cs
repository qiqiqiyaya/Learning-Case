using SimplePipeline.Rule;

namespace SimplePipeline.Core
{
    public class SubjectExecutedResult
    {
        public string Status { get; set; }

        public List<RuleCalculateResult> CalculateResults { get; set; }
    }
}
