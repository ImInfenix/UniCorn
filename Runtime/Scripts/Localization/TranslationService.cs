using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using UniCorn.Core;
using UniCorn.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UniCorn.Localization
{
	public class TranslationService : AbstractLocalizationService
	{
		private readonly ICoroutineHandler _coroutineHandler;

		private readonly Dictionary<string, string> _fallbackKeysMap = new();
		private readonly Dictionary<string, string> _currentKeysMap = new();

		public TranslationService(LocalizationSettings localizationSettings, ICoroutineHandler coroutineHandler) : base(localizationSettings)
		{
			_coroutineHandler = coroutineHandler;
		}

		#region Initialization

		public override void Initialize()
		{
			base.Initialize();

			LoadLanguageData(_fallbackLanguage, _fallbackKeysMap);
		}

		public override void SwitchToLanguage(SystemLanguage newTargetedLanguage)
		{
			if (newTargetedLanguage == _fallbackLanguage)
			{
				_currentKeysMap.Clear();
			}

			LoadLanguageData(newTargetedLanguage, _currentKeysMap);
		}

		private void LoadLanguageData(SystemLanguage language, Dictionary<string, string> mapToFill)
		{
			_coroutineHandler.RunCoroutineFromUniCorn(LoadLanguageDataCoroutine(language, mapToFill));
		}

		private IEnumerator LoadLanguageDataCoroutine(SystemLanguage language, Dictionary<string, string> mapToFill)
		{
			if (!AddressablesUtils.TryGetResourceLocation(language.GetUniCornAddressableKey(), typeof(TextAsset),
				    out IList<IResourceLocation> resourceLocation))
			{
				yield break;
			}

			AsyncOperationHandle<IList<TextAsset>> loadTextsOperation = Addressables.LoadAssetsAsync<TextAsset>(resourceLocation, null);

			yield return loadTextsOperation;

			FillKeyMap(loadTextsOperation.Result, mapToFill);

			Addressables.Release(loadTextsOperation);
		}

		private void FillKeyMap(IEnumerable<TextAsset> result, Dictionary<string, string> mapToFill)
		{
			mapToFill.Clear();

			foreach (TextAsset textAsset in result)
			{
				CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
				{
					Delimiter = ";",
					HasHeaderRecord = true
				};

				using StringReader stringReader = new StringReader(textAsset.text);
				using CsvReader csvReader = new CsvReader(stringReader, csvConfiguration);

				foreach (Translation translation in csvReader.GetRecords<Translation>())
				{
					mapToFill.Add(translation.Key, translation.Value);
				}
			}
		}

		#endregion

		public string Localize(string localizationKey)
		{
			if (_currentKeysMap.TryGetValue(localizationKey, out string localizedValue))
			{
				return localizedValue;
			}

			if (_fallbackKeysMap.TryGetValue(localizationKey, out localizedValue))
			{
				return localizedValue;
			}

			return localizationKey;
		}
	}
}
