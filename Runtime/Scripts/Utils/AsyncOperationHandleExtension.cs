using System;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UniCorn.Utils
{
    public static class AsyncOperationHandleExtension
    {
        public static void OnComplete<T>(this AsyncOperationHandle<T> asyncOperationHandle, Action<AsyncOperationHandle<T>> onAsyncOperationHandleCompleted)
        {
            asyncOperationHandle.Completed += loadedOperationHandle =>
            {
                if (loadedOperationHandle.IsValid())
                {
                    onAsyncOperationHandleCompleted.Invoke(loadedOperationHandle);
                }
            };
        }
    }
}
