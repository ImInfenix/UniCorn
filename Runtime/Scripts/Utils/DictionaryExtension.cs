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

		public static bool RemoveAndRetrieveIfExists<K, V>(this Dictionary<K, V> dictionary, K key, out V value)
		{
			if (!dictionary.TryGetValue(key, out value))
			{
				return false;
			}

			dictionary.Remove(key);
			return true;
		}

		public static V GetAndAddIfDoesntExist<K, V>(this Dictionary<K, V> dictionary, K key) where V : new()
		{
			dictionary.AddIfDoesntExist(key, new V());
			return dictionary[key];
		}
	}
}
