using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UniCorn.Utils
{
    public static class ListExtension
    {
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> list)
        {
            return list.Count == 0;
        }

        public static T GetLast<T>(this IReadOnlyList<T> list)
        {
            Assert.IsTrue(!list.IsEmpty());
            return list[^1];
        }

        public static T GetLastOrDefault<T>(this IReadOnlyList<T> list)
        {
            return list.IsEmpty() ? default : list[^1];
        }
    }
}
