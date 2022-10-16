using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.Desgin
{
    public class DynamicProxyFactory
    {
        protected DynamicProxy dynamicProxy = new DynamicProxy();

        public void AddProxy<T>(Action<object?[]?> beftr, Action<object?[]?> after)
        {
            dynamicProxy.AddMethod<T>(beftr, after);
        }
    }
}
