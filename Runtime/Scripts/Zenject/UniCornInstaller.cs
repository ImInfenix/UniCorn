using UniCorn.Input;
using UnityEngine;
using Zenject;

namespace Orion.Installers
{
    public class UniCornInstaller : MonoInstaller
    {
        [SerializeField] private InputDefinition inputDefinition;

        public override void InstallBindings()
        {
            Container.Bind<InputDefinition>().FromInstance(inputDefinition).AsSingle();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}