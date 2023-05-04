using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public abstract class Scope
    {
        public int Id { get; set; }

        /// <summary>
        /// 满足条件，且享有 返回 true
        /// 满足条件，且不享有 返回 false
        /// "所有条件不满足， 返回 false"
        /// </summary>
        public bool Benefit { get; set; }

        public abstract Task<bool> IsSatisfy(Employee employee, ScopeCheckContext context);
    }
}
