using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.Desgin
{
    public  class DynamicProxyFactory<T>
    {
     public   static DynamicProxy dynamicProxy;
        static Dictionary<string,T> dic;
        static DynamicProxyFactory() { dynamicProxy = new DynamicProxy();dic = new Dictionary<string, T>(); }

        public static T AddProxy(Action<object?[]?> beftr, Action<object?[]?> after)
        {
            var key= typeof(T).Name + beftr.GetHashCode() + after.GetHashCode();
            if (!dic.ContainsKey(key))
            {
                dic.TryAdd(key,dynamicProxy.AddMethod<T>(dynamicProxy.Before, dynamicProxy.After));
            }
            return dic[key];
        }
    }
}
