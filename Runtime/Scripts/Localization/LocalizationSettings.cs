using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniCorn.Localization
{
    [CreateAssetMenu(fileName = nameof(LocalizationSettings), menuName = nameof(UniCorn) + "/" + nameof(LocalizationSettings))]
    public class LocalizationSettings : ScriptableObject
    {
        [SerializeField] private SystemLanguage _defaultLanguage = SystemLanguage.English;
        
        [SerializeField] private List<SystemLanguage> _supportedLanguages;

        public SystemLanguage DefaultLanguage => _defaultLanguage;
        public List<SystemLanguage> SupportedLanguages => _supportedLanguages;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_supportedLanguages == null || _supportedLanguages.Count < 1)
            {
                _defaultLanguage = SystemLanguage.English;
                return;
            }

            if (!_supportedLanguages.Contains(_defaultLanguage))
            {
                _defaultLanguage = _supportedLanguages.First();
            }
        }
#endif
    }
}
