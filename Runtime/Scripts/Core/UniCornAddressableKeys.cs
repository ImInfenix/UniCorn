using UnityEngine;

namespace UniCorn.Core
{
    public static class UniCornAddressableKeys
    {
        private static string UnicornAddressablePrefix => "unicorn";
        
        public static string GetUniCornAddressableKey(this SystemLanguage language)
        {
            return $"{UnicornAddressablePrefix}/language_{language.ToString().ToLowerInvariant()}";
        }
    }
}
