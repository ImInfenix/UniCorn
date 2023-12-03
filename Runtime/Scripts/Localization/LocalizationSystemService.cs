using UnityEngine;
using UnityEngine.Assertions;

namespace UniCorn.Localization
{
	public class LocalizationSystemService : AbstractLocalizationService
	{
		private readonly TranslationService _translationService;

		public LocalizationSystemService(LocalizationSettings localizationSettings, TranslationService translationService) : base(localizationSettings)
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
