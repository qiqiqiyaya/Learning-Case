using Newtonsoft.Json;
using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public class ScopeCheckContext
    {
        public ScopeCheckContext(HandlerExecutionContext handlerExecutionContext)
        {
            HandlerExecutionContext = handlerExecutionContext;
        }

        /// <summary>
        /// 不暴露给scope直接访问
        /// </summary>
        protected HandlerExecutionContext HandlerExecutionContext { get; }

        public Subject Subject => HandlerExecutionContext.Subject;

        public Employee Employee => HandlerExecutionContext.Employee;

        public List<Scope> Scopes => Subject.Scopes;

        public T GetRequiredService<T>()
            where T : notnull
        {
            return HandlerExecutionContext.GetRequiredService<T>();
        }

        public void AddSuccessedLog(Scope scope)
        {
            var name = scope.GetType().Name;
            var json = JsonConvert.SerializeObject(scope);
            HandlerExecutionContext.CheckLogs.Add(ScopeCheckLog.SuccessedLog(name, json));
        }

        public void AddFailedLog(Scope scope)
        {
            var name = scope.GetType().Name;
            var json = JsonConvert.SerializeObject(scope);
            HandlerExecutionContext.CheckLogs.Add(ScopeCheckLog.FailedLog(name, json));
        }
    }
}
