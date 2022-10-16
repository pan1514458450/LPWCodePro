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


        public static void AddProxyDynamic(this IServiceCollection services)
        {
           serviceProvider= services.BuildServiceProvider();
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
