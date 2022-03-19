using UnityEngine;
using TMPro;

namespace UniCorn.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizationKey;
        
        private TranslationService _translationService;
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

#if UNICORN_FOR_ZENJECT
        [Zenject.Inject]
#endif
        public void InitializeDependencies(TranslationService translationService)
        {
            _translationService = translationService;
        }

         private void OnEnable()
         {
             _text.text = _translationService.Localize(_localizationKey);
         }
    }
}
