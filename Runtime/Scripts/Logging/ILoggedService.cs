using UniCorn.Core;

namespace UniCorn.Logging
{
    public interface ILoggedService : IService
    {
        public string GetServiceName() => GetType().Name;
        public string[] GetLogContent();
    }
}
