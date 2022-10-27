using System.Reflection;

namespace LPWService.Desgin
{
    public class DynamicProxy : DispatchProxy
    {
        public DynamicProxy()
        {
            Console.WriteLine("zhixing");
            Console.WriteLine(this.GetType().GetHashCode());
        }
        /// <summary>
        /// 目标类
        /// </summary>
        /// <summary>
        /// 执行前
        /// </summary>
        private Action<object?[]?> _before { get; set; }
        /// <summary>
        /// 执行后
        /// </summary>
        private Action<object?[]?> _after { get; set; }
        /// <summary>
        /// 返回值，返回值可能在完成执行前洄游至
        /// </summary>
        private object? retval { get; set; }
        protected sealed override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            Console.WriteLine(this.GetType().GetHashCode());

            if (_before != null)
                _before(args);
            if (retval != null)
                return retval;
            retval = targetMethod.Invoke(InjectionClass.GetService(targetMethod.DeclaringType), args);
            if (_after != null)
                _after(args);
            return retval;
        }
        private string id = "1";
        public T AddMethod<T>(Action<object?[]?> before, Action<object?[]?> after)
        {
            this.id = "2";
            object proxy = Create<T, DynamicProxy>();
            ((DynamicProxy)proxy)._before=before;   
            ((DynamicProxy)proxy)._after=after;   
            Console.WriteLine(this.GetType().GetHashCode());

            this._after = after;
            return (T)proxy;
        }
        public void Before(object?[]? objects)
        {
            Console.WriteLine("执行前");
        }
        public void After(object?[]? objects)
        {
            Console.WriteLine("执行后");
        }

    }
}
