using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UniCorn.Core
{
    public abstract class AbstractAsynchronouslyLoadedService : IService
    {
        private readonly List<AsyncOperationHandle> _asyncOperationHandles = new();

        private bool _isLoadingDone;

        public virtual void Initialize()
        {
            
        }

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

        protected void RegisterAsynchronousOperation(AsyncOperationHandle operationHandle)
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

        protected abstract void OnAsyncOperationsCompleted();
    }
}
