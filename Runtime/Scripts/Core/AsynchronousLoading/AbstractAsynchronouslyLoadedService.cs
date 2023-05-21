using System;
using System.Collections.Generic;
using UniCorn.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UniCorn.Core.AsynchronousLoading
{
	public abstract class AbstractAsynchronouslyLoadedService : IService
	{
		private readonly List<AsyncOperationHandle> _asyncOperationHandles = new();

		public event Action OnLoadingDone;

		public bool IsLoadingDone { get; private set; }

		public abstract void Initialize();

		public virtual void Dispose()
		{
			foreach (AsyncOperationHandle operationHandle in _asyncOperationHandles)
			{
				if (operationHandle.IsValid())
				{
					Addressables.Release(operationHandle);
				}
			}
		}

		protected void LoadAssetsAsync<T>(string key, Action<AsyncOperationHandle<IList<T>>> onAssetsLoaded)
		{
			if (!AddressablesUtils.TryGetResourceLocation(key, typeof(T), out IList<IResourceLocation> resourceLocation))
			{
				return;
			}

			AsyncOperationHandle<IList<T>> loadAssetsOperation = Addressables.LoadAssetsAsync<T>(resourceLocation, null);

			loadAssetsOperation.OnComplete(onAssetsLoaded);

			RegisterAsynchronousOperation(loadAssetsOperation);
		}

		private void RegisterAsynchronousOperation(AsyncOperationHandle operationHandle)
		{
			operationHandle.Completed += OnAsyncOperationHandleCompleted;

			_asyncOperationHandles.Add(operationHandle);
		}

		private void OnAsyncOperationHandleCompleted(AsyncOperationHandle currentOperationHandle)
		{
			if (!currentOperationHandle.IsValid())
			{
				Debug.LogWarning($"The following async operation completed but is invalid: {currentOperationHandle}");
				return;
			}

			foreach (AsyncOperationHandle operationHandle in _asyncOperationHandles)
			{
				if (!operationHandle.IsDone || !operationHandle.IsValid())
				{
					return;
				}
			}

			OnAsyncOperationsCompleted();
		}

		private void OnAsyncOperationsCompleted()
		{
			foreach (AsyncOperationHandle operationHandle in _asyncOperationHandles)
			{
				if (!operationHandle.IsDone)
				{
					return;
				}
			}

			IsLoadingDone = true;
			OnLoadingDone?.Invoke();
			OnLoadingDone = null;
		}
	}
}
