using Volo.Abp.Auditing;

namespace ConsoleApp1
{
    internal class TestAuditService : IAuditingStore
    {
        public virtual Task SaveAsync(AuditLogInfo auditInfo)
        {

            return Task.CompletedTask;
        }
    }
}
