using System;
using System.Collections.Generic;

namespace UniCorn.Core.AsynchronousLoading
{
    public class AbstractAsynchronouslyLoadedServiceSynchronizer
    {
        private readonly List<AbstractAsynchronouslyLoadedService> _servicesToSynchronize = new();

        private event Action OnSynchronizationComplete;

        public void RegisterAsynchronousLoadedService(AbstractAsynchronouslyLoadedService serviceToRegister)
        {
            _servicesToSynchronize.Add(serviceToRegister);
            serviceToRegister.OnLoadingDone += SynchronizeServicesIfLoadingComplete;
        }

        public void UnregisterAsynchronousLoadedService(AbstractAsynchronouslyLoadedService serviceToUnregister)
        {
            _servicesToSynchronize.Remove(serviceToUnregister);
            serviceToUnregister.OnLoadingDone -= SynchronizeServicesIfLoadingComplete;
        }

        public void ExecuteOnSynchronizationComplete(Action action)
        {
            if (IsSynchronizationComplete())
            {
                action?.Invoke();
                return;
            }

            OnSynchronizationComplete += action;
        }

        private bool IsSynchronizationComplete()
        {
            foreach (AbstractAsynchronouslyLoadedService asynchronouslyLoadedService in _servicesToSynchronize)
            {
                if (!asynchronouslyLoadedService.IsLoadingDone)
                {
                    return false;
                }
            }

            return true;
        }

        private void SynchronizeServicesIfLoadingComplete()
        {
            if (!IsSynchronizationComplete())
            {
                return;
            }

            OnSynchronizationComplete?.Invoke();
            OnSynchronizationComplete = null;
        }
    }
}
