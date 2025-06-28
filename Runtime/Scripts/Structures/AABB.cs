using UnityEngine;

namespace UniCorn.Structures
{
    public struct AABB
    {
        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }

        public Vector2 Center => (Min + Max) / 2f;
        public Vector2 Size => Max - Min;

        public AABB OrderedToAxis()
        {
            float minX = Min.x;
            float maxX = Max.x;
            float minY = Min.y;
            float maxY = Max.y;

            if (minX > maxX)
            {
                (minX, maxX) = (maxX, minX);
            }

            if (minY > maxY)
            {
                (minY, maxY) = (maxY, minY);
            }

            return new AABB { Min = new Vector2(minX, minY), Max = new Vector2(maxX, maxY) };
        }
    }
}
