using UnityEngine;

namespace UniCorn.Utils
{
    public static class FloatUtils
    {
        public const float PRECISION = 0.001f;

        public static bool AreAlmostEqual(float value1, float value2)
        {
            return Mathf.Abs(value1 - value2) < PRECISION;
        }
    }
}
