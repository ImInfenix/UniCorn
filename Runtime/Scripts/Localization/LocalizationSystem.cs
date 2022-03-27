using UnityEngine;
using UnityEngine.Assertions;

namespace UniCorn.Localization
{
    public class LocalizationSystem : AbstractLocalizationService
    {
        private readonly TranslationService _translationService;

        public LocalizationSystem(LocalizationSettings localizationSettings, TranslationService translationService) : base(localizationSettings)
        {
            _translationService = translationService;
        }

        public override void SwitchToLanguage(SystemLanguage newTargetedLanguage)
        {
            Assert.IsTrue(_localizationSettings.SupportedLanguages.Contains(newTargetedLanguage));
            
            _translationService.SwitchToLanguage(newTargetedLanguage);
        }
    }
}
