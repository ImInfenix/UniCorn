﻿using UniCorn.Core;
using UnityEngine;

namespace UniCorn.Localization
{
	public abstract class AbstractLocalizationService : IService
	{
		protected readonly LocalizationSettings _localizationSettings;

		protected SystemLanguage _fallbackLanguage;

		protected AbstractLocalizationService(LocalizationSettings localizationSettings)
		{
			_localizationSettings = localizationSettings;
		}

		public virtual void Initialize()
		{
			_fallbackLanguage = _localizationSettings.DefaultLanguage;
		}

		public virtual void Dispose()
		{
		}

		public abstract void SwitchToLanguage(SystemLanguage newTargetedLanguage);
	}
}
