namespace ExtractData
{
    public static class ControlExtensions
    {
        /// <summary>
        /// 在安全线程内，控件属性设置
        /// </summary>
        /// <typeparam name="TControl"><see cref="Control"/></typeparam>
        /// <param name="this">控件</param>
        /// <param name="action">操作</param>
        public static void SetPropertyThreadSafe<TControl>(this TControl @this, Action<TControl> action)
            where TControl : Control
        {
            if (@this.IsDisposed) return;

            if (@this.InvokeRequired)
            {
                @this.Invoke(() =>
                {
                    action(@this);
                });
            }
            else
            {
                action(@this);
            }
        }
    }
}
