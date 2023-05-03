using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public interface IScopeCheckerService
    {
        Task<ScopeCheckResult> CheckAsync(Subject subject, Employee employee);
    }
}
