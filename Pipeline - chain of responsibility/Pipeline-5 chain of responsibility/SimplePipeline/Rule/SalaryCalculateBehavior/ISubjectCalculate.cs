namespace SimplePipeline.Rule.SalaryCalculateBehavior
{
    public interface ISubjectCalculate
    {
        /// <summary>
        /// 需要运行子级管道，运算subject
        /// </summary>
        /// <returns></returns>
        Task SubjectCalculateAsync();
    }
}
