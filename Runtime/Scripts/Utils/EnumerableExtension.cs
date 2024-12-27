using System.Collections.Generic;

namespace UniCorn.Utils
{
    public static class EnumerableExtension
    {
        public static T GetElementOfType<T, U>(this IEnumerable<U> collection) where T : U
        {
            foreach (U element in collection)
            {
                if (element is T castedElement)
                {
                    return castedElement;
                }
            }

            return default;
        }
    }
}
