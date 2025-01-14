﻿using UnityEngine;

namespace UniCorn.Utils
{
    public static class Vector2IntExtension
    {
        public static Vector2Int Abs(this Vector2Int vector2)
        {
            return new Vector2Int(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
        }

        public static Vector3Int X0Z(this Vector2Int vector2)
        {
            return new Vector3Int(vector2.x, 0, vector2.y);
        }
    }
}
