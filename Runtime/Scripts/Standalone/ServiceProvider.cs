using System;
using System.Collections.Generic;

namespace UniCorn.Standalone
{
    public static class ServiceProvider
    {
        private static readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        static ServiceProvider()
        {
        }

        public static void RegisterService<T>(T service) where T : IService
        {
            _services.Add(typeof(T), service);
            service.Initialize();
        }

        public static void UnregisterService<T>() where T : IService
        {
            _services.Remove(typeof(T));
        }

        public static void GetService<T>(out T service) where T : IService
        {
            service = (T) _services[typeof(T)];
        }
    }
}
