using UnityEngine;

namespace UniCorn.Core
{
    public static class UniCornAddressableKeys
    {
        private const string UNICORN_ADDRESSABLE_PREFIX = "unicorn";

        public static string UNICORN_ADDRESSABLE_INPUT_DEFINITIONS_KEY => GetUniCornAddressableKey("input_definitions");
        
        public static string GetUniCornAddressableKey(this SystemLanguage language)
        {
            return GetUniCornAddressableKey($"language_{language.ToString().ToLowerInvariant()}");
        }

        private static string GetUniCornAddressableKey(string key)
        {
            return $"{UNICORN_ADDRESSABLE_PREFIX}/{key}";
        }
    }
}
