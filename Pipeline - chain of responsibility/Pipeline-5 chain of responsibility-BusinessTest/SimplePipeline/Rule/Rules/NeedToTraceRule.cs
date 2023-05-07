using SimplePipeline.Rule.SalaryCalculateBehavior;

namespace SimplePipeline.Rule.Rules
{
    /// <summary>
    /// 需要追溯
    /// </summary>
    public class NeedToTraceRule : Rule,ISubjectCalculate
    {
        public Task SubjectCalculateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
