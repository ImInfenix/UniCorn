using UnityEngine;

namespace UniCorn.Utils
{
    public static class Vector2Extension
    {
        public static Vector3 X0Z(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
    }
}
