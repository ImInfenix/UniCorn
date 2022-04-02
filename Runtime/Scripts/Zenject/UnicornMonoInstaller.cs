using UniCorn.Core.AsynchronousLoading;
using Zenject;

namespace UniCorn.Zenject
{
    public class UnicornMonoInstaller<TDerived> : MonoInstaller<TDerived> where TDerived : MonoInstaller<TDerived>
    {
        public override void InstallBindings()
        {
            Container.BindExecutionOrder<AbstractAsynchronouslyLoadedService>(-100);
        }
    }
}
