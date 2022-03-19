using UniCorn.Core;
using UniCorn.Input;
using UniCorn.Localization;
using UnityEngine;
using Zenject;

namespace UniCorn.Zenject
{
    public class UniCornInstaller : MonoInstaller<UniCornInstaller>
    {
        [SerializeField] private InputDefinition _inputDefinition;
        [SerializeField] private LocalizationSettings _localizationSettings;

        public override void InstallBindings()
        {
            Container.Bind<InputDefinition>().FromInstance(_inputDefinition).AsSingle();
            Container.Bind<LocalizationSettings>().FromInstance(_localizationSettings).AsSingle();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TranslationService>().AsSingle();
            
            Container.Bind<UniCornMonoBehaviour>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
