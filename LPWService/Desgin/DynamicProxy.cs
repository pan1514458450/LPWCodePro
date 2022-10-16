using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.Desgin
{
    public class DynamicProxy : DispatchProxy
    {
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
            if(_before != null)
                _before(args);
            if(retval !=null)
                return retval;
                retval= targetMethod.Invoke(InjectionClass.GetService(targetMethod.DeclaringType),args);
            if(_after!=null)
                _after(args);
            return retval;
        }
        public T AddMethod<T>(Action<object?[]?> before, Action<object?[]? > after)
        {
            object proxy = Create<T, DynamicProxy>();
            this._before = before;
            this._after = after;
            return (T)proxy;
        }
        internal void Before(object?[]? objects)
        {
            Console.WriteLine("执行前");
        }
        internal void After(object?[]? objects)
        {
            Console.WriteLine("执行后");
        }

    }
}
