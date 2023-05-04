namespace SimplePipeline.Rule
{
    public enum ExecuteBehavior
    {
        /// <summary>
        /// 直接完成
        /// </summary>
        Done,

        /// <summary>
        /// 继续执行
        /// </summary>
        Continue,

        /// <summary>
        /// 计算后完成
        /// </summary>
        CalculateThenDone,


    }
}
