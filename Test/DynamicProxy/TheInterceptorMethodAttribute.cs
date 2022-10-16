using ExpCode.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.DynamicProxy
{
    public class TheInterceptorMethodAttribute : DispatchProxy
    {

        protected sealed override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            _afterAction(args);
            if (!next)
                return value;
            var obj = targetMethod.Invoke(IocClass.GetServer(targetMethod.DeclaringType), args);
            _beforeAction(args);
            return obj;
        }
        private object?[]? objects;
        private Action<object?[]?>? _afterAction { get; set; }  // 动作之后执行
        private Action<object?[]?>? _beforeAction { get; set; } // 动作之前执行
        public bool next { get; set; } = true;
        private object value;
        public T CreateProxy<T>(Action<object?[]?>? _afterAction, Action<object?[]?>? _beforeAction)
        {
            object proxy = Create<T, TheInterceptorMethodAttribute>();
            var pro = (TheInterceptorMethodAttribute)proxy;
            pro._afterAction = _afterAction;
            pro._beforeAction = _beforeAction;
            return (T)proxy;
        }

        public virtual void cs(object?[]? args)
        {
        }
        public virtual void csd(object?[]? args)
        {
        }

    }
    public class ds : TheInterceptorMethodAttribute
    {

    }
}
