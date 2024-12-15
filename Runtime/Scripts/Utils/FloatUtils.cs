using System;
using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float BranchlessConditionalOneMinus(bool oneMinus, float value)
        {
            int maxValueAtCenterInt = Convert.ToInt32(oneMinus);
            return (1f - value) * maxValueAtCenterInt + value * (1f - maxValueAtCenterInt);
        }
    }
}
