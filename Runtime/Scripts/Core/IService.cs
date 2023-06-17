using System;
#if UNICORN_FOR_ZENJECT
using Zenject;
#else
using UniCorn.Standalone;
#endif

namespace UniCorn.Core
{
	public interface IService : IInitializable, IDisposable
	{
		void IInitializable.Initialize()
		{
		}

		void IDisposable.Dispose()
		{
		}
	}
}
