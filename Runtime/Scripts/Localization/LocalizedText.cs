using TMPro;
using UnityEngine;
#if UNICORN_FOR_ZENJECT
using Zenject;
#endif

namespace UniCorn.Localization
{
	[RequireComponent(typeof(TMP_Text))]
	public class LocalizedText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _localizedText;
		[SerializeField] private string _localizationKey;

		private TranslationService _translationService;

		private void Awake()
		{
			_localizedText = GetComponent<TMP_Text>();
		}

#if UNICORN_FOR_ZENJECT
		[Inject]
#endif
		public void InitializeDependencies(TranslationService translationService)
		{
			_translationService = translationService;
		}

		private void OnEnable()
		{
			_localizedText.text = _translationService.Localize(_localizationKey);
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (_localizedText == null)
			{
				_localizedText = GetComponent<TMP_Text>();
			}

			if (_localizedText != null)
			{
				_localizedText.text = _localizationKey;
			}
		}
#endif
	}
}
