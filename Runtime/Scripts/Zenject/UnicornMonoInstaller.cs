using UniCorn.Core;
using UniCorn.Core.AsynchronousLoading;
using Zenject;

namespace UniCorn.Zenject
{
    public class UnicornMonoInstaller<TDerived> : MonoInstaller<TDerived> where TDerived : MonoInstaller<TDerived>
    {
        public override void InstallBindings()
        {
            foreach (AbstractLayout abstractLayout in FindObjectsOfType<AbstractLayout>(true))
            {
                Container.Bind(abstractLayout.GetType()).FromInstance(abstractLayout).AsSingle();
            }

            Container.BindExecutionOrder<AbstractAsynchronouslyLoadedService>(-100);
        }
    }
}
