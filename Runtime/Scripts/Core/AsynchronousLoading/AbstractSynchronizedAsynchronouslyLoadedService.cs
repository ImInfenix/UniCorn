namespace UniCorn.Core.AsynchronousLoading
{
    public class AbstractSynchronizedAsynchronouslyLoadedService : AbstractAsynchronouslyLoadedService
    {
        private readonly AbstractAsynchronouslyLoadedServiceSynchronizer _synchronizer;

        protected AbstractSynchronizedAsynchronouslyLoadedService(AbstractAsynchronouslyLoadedServiceSynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }

        public override void Initialize()
        {
            base.Initialize();

            _synchronizer.RegisterAsynchronousLoadedService(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            _synchronizer.UnregisterAsynchronousLoadedService(this);
        }
    }
}
