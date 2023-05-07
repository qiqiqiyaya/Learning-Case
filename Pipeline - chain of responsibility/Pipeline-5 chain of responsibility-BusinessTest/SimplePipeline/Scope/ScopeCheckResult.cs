namespace SimplePipeline.Scope
{
    public class ScopeCheckResult
    {
        public bool Successed { get; set; }

        public string CheckLog { get; set; } = default!;

        public static ScopeCheckResult Success = new ScopeCheckResult() { Successed = true };
    }
}
