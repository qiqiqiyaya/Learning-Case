using SimplePipeline.Core;

namespace SimplePipeline.Scope.Services
{
    public interface IScopeCheckerService
    {
        Task<ScopeCheckResult> CheckAsync(ScopeCheckContext context);
    }
}
