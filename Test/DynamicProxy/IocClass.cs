namespace Test.DynamicProxy
{
    internal static class IocClass
    {
        private static IServiceProvider? serviceProvider { get; set; }
        private readonly static IServiceCollection services = new ServiceCollection();

        public static IServiceCollection AddSingleton(Type type, Type type1)
        {
            serviceProvider = services.AddSingleton(type, type1).BuildServiceProvider();
            return services;
        }
        public static void AddContainer(IServiceCollection services)
        {
            serviceProvider = services.BuildServiceProvider();
        }
        public static object GetServer(Type type)
        {
            return serviceProvider.GetService(type);
        }
        public static IServiceCollection AddDyamincProxy<T>(this IServiceCollection services1)
        {
            var d = ProxyFactory.AddProxy<T>();
            //if (cs != null)
            AddSingleton(typeof(T), d.GetType());
            return services;
        }
        public static T GetServer<T>()
        {
            return serviceProvider.GetService<T>();
        }

    }
}
