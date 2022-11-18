using System.Collections.Generic;
using UniCorn.Core;

namespace UniCorn.Logging
{
    public class AbstractLoggedServiceInjector : IService
    {
        private readonly IReadOnlyList<ILoggedService> _loggedServices;

        protected AbstractLoggedServiceInjector(IReadOnlyList<ILoggedService> loggedServices)
        {
            _loggedServices = loggedServices;
        }

        public void Initialize()
        {
            foreach (ILoggedService loggedService in _loggedServices)
            {
                ServiceLogger.Register(loggedService);
            }
        }

        public void Dispose()
        {
            foreach (ILoggedService loggedService in _loggedServices)
            {
                ServiceLogger.Unregister(loggedService);
            }
        }
    }
}
