using UnityEngine;

namespace UniCorn.Structures
{
    public struct AABB
    {
        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }

        public Vector2 Center => (Min + Max) / 2f;
        public Vector2 Size => Max - Min;
    }
}
