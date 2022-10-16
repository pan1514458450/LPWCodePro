using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.Desgin
{
    public static class InjectionClass
    {
        private static IServiceProvider serviceProvider { get; set; }


        public static void AddProxyDynamic(this IServiceCollection services,Type type,Type type2)
        {
           
            if (type2.GetInterface(type.Name) is null) throw  new ArgumentNullException("为继承该接口");
           serviceProvider= services.BuildServiceProvider();
        }
        public static void AddProxyDynamic<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
        {
            serviceProvider = services.BuildServiceProvider();
        }

        internal static object GetService(Type type)
        {
           var ser_type= serviceProvider.GetService(type);
            if (ser_type == null)
                throw new Exception("未注入接口和实现类");
            return ser_type;
        }
        
    }
}
