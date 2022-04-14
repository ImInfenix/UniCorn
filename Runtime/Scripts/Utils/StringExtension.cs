using System.Collections.Generic;

namespace UniCorn.Utils
{
    public static class StringExtension
    {
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count == 0;
        }
    }
}