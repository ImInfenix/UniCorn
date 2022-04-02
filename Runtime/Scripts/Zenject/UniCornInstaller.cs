using UniCorn.Core;
using UniCorn.Input;
using UniCorn.Localization;
using UniCorn.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Zenject
{
    public class UniCornInstaller : UnicornMonoInstaller<UniCornInstaller>
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private LocalizationSettings _localizationSettings;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.Bind<LocalizationSettings>().FromInstance(_localizationSettings).AsSingle();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<NavigationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TranslationService>().AsSingle();

            Container.Bind(typeof(IInputActionCollection), typeof(IInputActionCollection2)).FromInstance(_inputActionAsset).AsSingle();

            Container.Bind<UniCornMonoBehaviour>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
