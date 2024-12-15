using UnityEngine;

namespace UniCorn.Utils
{
    public static class Vector2Extension
    {
        public static Vector2 Abs(this Vector2 vector2)
        {
            return new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
        }

        public static Vector3 X0Z(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
    }
}
