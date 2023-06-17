#if UNICORN_FOR_ZENJECT
using Zenject;
#else
using UniCorn.Standalone;
#endif

namespace UniCorn.Core
{
	public interface ITickableService : IService, ITickable
	{
	}
}
