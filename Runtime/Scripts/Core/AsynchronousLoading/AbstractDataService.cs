using System.Collections.Generic;
using UnityEngine;

namespace UniCorn.Core.AsynchronousLoading
{
	public abstract class AbstractDataService<DATA_TYPE> : AbstractSynchronizedAsynchronouslyLoadedService where DATA_TYPE : ScriptableObject
	{
		private readonly List<DATA_TYPE> _loadedData = new();

		protected abstract string DataAddressableKey { get; }

		public IReadOnlyList<DATA_TYPE> LoadedData => _loadedData;

		protected AbstractDataService(AbstractAsynchronouslyLoadedServiceSynchronizer synchronizer) : base(synchronizer)
		{
		}

		protected override void InitializeInternal()
		{
			LoadAssetsAsync<DATA_TYPE>(DataAddressableKey, operationHandle =>
			{
				foreach (DATA_TYPE buildingBlueprintData in operationHandle.Result)
				{
					_loadedData.Add(buildingBlueprintData);
				}
			});
		}

		protected override void DisposeInternal()
		{
		}
	}
}
