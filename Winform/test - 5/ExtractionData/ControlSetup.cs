namespace ExtractionData
{
    public static class ControlSetup
    {
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

        //private delegate void SetPropertyThreadSafeDelegate<TResult>(
        //    Control @this,
        //    Expression<Func<TResult>> property,
        //    TResult value);

        //public static void SetPropertyThreadSafe<TResult>(
        //    this Control @this,
        //    Expression<Func<TResult>> property,
        //    [DisallowNull] TResult value)
        //{
        //    if (value == null) throw new ArgumentNullException(nameof(value));

        //    var propertyInfo = (property.Body as MemberExpression)?.Member as PropertyInfo;

        //    //var aa = @this.GetType().IsSubclassOf(propertyInfo.ReflectedType);
        //    if (propertyInfo == null ||
        //        @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
        //    {
        //        throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
        //    }

        //    if (@this.InvokeRequired)
        //    {
        //        @this.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe!),new object[] { @this, property, value });
        //    }
        //    else
        //    {
        //        @this.GetType().InvokeMember(
        //            propertyInfo.Name,
        //            BindingFlags.SetProperty,
        //            null,
        //            @this,
        //            new object[] { value });
        //    }
        //}
    }
}
