namespace UniCorn.Core.AsynchronousLoading
{
    public abstract class AbstractSynchronizedAsynchronouslyLoadedService : AbstractAsynchronouslyLoadedService
    {
        private readonly AbstractAsynchronouslyLoadedServiceSynchronizer _synchronizer;

        protected AbstractSynchronizedAsynchronouslyLoadedService(AbstractAsynchronouslyLoadedServiceSynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }

        public sealed override void Initialize()
        {
            _synchronizer.RegisterAsynchronousLoadedService(this);
            InitializeInternal();
        }

        protected abstract void InitializeInternal();

        public sealed override void Dispose()
        {
            DisposeInternal();
            _synchronizer.UnregisterAsynchronousLoadedService(this);
            base.Dispose();
        }

        protected abstract void DisposeInternal();
    }
}
