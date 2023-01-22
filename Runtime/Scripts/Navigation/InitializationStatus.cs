using System;

namespace UniCorn.Navigation
{
	[Flags]
	public enum InitializationStatus
	{
		NotInitialized,
		Started,
		EnabledOnce,
	}
}
