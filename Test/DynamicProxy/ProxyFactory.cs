using ExpCode.Model;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Reflection;

namespace Test.DynamicProxy
{
    public static class ProxyFactory<T>
    {


        private static Dictionary<Type, T> dic;
        private static readonly TheInterceptorMethodAttribute _theinter;
        static ProxyFactory()
        {
            _theinter = new TheInterceptorMethodAttribute();
            dic = new Dictionary<Type, T>();
        }

        public static T AddDynamicProxy()
        {

            return _theinter.CreateProxy<T>(_theinter.cs, _theinter.csd);
            //if (!dic.ContainsKey(typeof(T)))
            //{
            // dic.Add(typeof(T), DispatchProxy.Create<T, TheInterceptorMethodAttribute>());
            //}
            //return dic[typeof(T)];
        }

    }

    public static class ProxyFactory
    {
        private readonly static TheInterceptorMethodAttribute _theinter;
        static ProxyFactory()
        {
            _theinter = new TheInterceptorMethodAttribute();
        }
        public static T AddProxy<T>()
        {
            return _theinter.CreateProxy<T>(_theinter.cs, _theinter.csd);
        }
    }
}
