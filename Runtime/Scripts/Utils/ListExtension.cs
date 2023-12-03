using System.Collections.Generic;

namespace UniCorn.Utils
{
	public static class ListExtension
	{
		public static bool IsEmpty<T>(this IReadOnlyCollection<T> list)
		{
			return list.Count == 0;
		}

		public static T GetLast<T>(this List<T> list)
		{
			return list[^1];
		}

		public static T GetLastOrDefault<T>(this List<T> list)
		{
			return list.Count == 0 ? default : list[^1];
		}
	}
}
