using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UniCorn.Utils
{
    public static class RectUtils
    {
        public static Rect FromCenterAndSize(Vector2 center, Vector2 size)
        {
            return new Rect(center - size / 2f, size);
        }

        public static bool Contains(this Rect rect, Rect other)
        {
            return rect.Contains(other.position) && rect.Contains(other.position + other.size);
        }
    }
}
