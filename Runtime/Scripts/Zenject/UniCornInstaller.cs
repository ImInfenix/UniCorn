using UniCorn.Core;
using UniCorn.Input;
using UnityEngine;
using Zenject;

namespace UniCorn.Zenject
{
    public class UniCornInstaller : MonoInstaller
    {
        [SerializeField] private InputDefinition _inputDefinition;

        public override void InstallBindings()
        {
            Container.Bind<InputDefinition>().FromInstance(_inputDefinition).AsSingle();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}
