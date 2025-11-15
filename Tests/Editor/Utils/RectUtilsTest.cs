using NUnit.Framework;
using UniCorn.Utils;
using UnityEngine;

namespace UniCorn.Tests.Editor.Utils
{
    public class RectUtilsTest
    {
        [TestCase(0, 0, 1, 1, -0.5f, -0.5f)]
        [TestCase(1, 1, 2, 2, 0, 0)]
        [TestCase(-1, -1, 2, 2, -2, -2)]
        public void FromCenterAndSize(params float[] elements)
        {
            Assert.AreEqual(6, elements.Length);
            Vector2 center = new Vector2(elements[0], elements[1]);
            Vector2 size = new Vector2(elements[2], elements[3]);
            Vector2 expectedPosition = new Vector2(elements[4], elements[5]);
            Rect rect = RectUtils.FromCenterAndSize(center, size);
            Assert.AreEqual(expectedPosition, rect.position);
        }

        [TestCase(0, 0, 1, 1, 0, 0, 1, 1, ExpectedResult = false)]
        [TestCase(0, 0, 5, 5, 2, 2, 1, 1, ExpectedResult = true)]
        [TestCase(0, 0, 1, 1, 0.5f, 0, 1, 1, ExpectedResult = false)]
        [TestCase(0, 0, 1, 1, -0.5f, 0, 1, 1, ExpectedResult = false)]
        [TestCase(0, 0, 1, 1, 0, 0.5f, 1, 1, ExpectedResult = false)]
        [TestCase(0, 0, 1, 1, 0, -0.5f, 1, 1, ExpectedResult = false)]
        public bool ContainsOtherRect(params float[] elements)
        {
            Vector2 containerPosition = new Vector2(elements[0], elements[1]);
            Vector2 containerSize = new Vector2(elements[2], elements[3]);
            Rect container = new Rect(containerPosition, containerSize);

            Vector2 otherPosition = new Vector2(elements[4], elements[5]);
            Vector2 otherSize = new Vector2(elements[6], elements[7]);
            Rect other = new Rect(otherPosition, otherSize);

            return container.Contains(other);
        }
    }
}
