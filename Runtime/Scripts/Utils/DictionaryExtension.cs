using System.Collections.Generic;

namespace UniCorn.Utils
{
    public static class DictionaryExtension
    {
        public static void AddIfDoesntExist<K, V>(this Dictionary<K, V> dictionary, K key, V value)
        {
            if (dictionary.ContainsKey(key))
            {
                return;
            }

            dictionary.Add(key, value);
        }
    }
}
