using NUnit.Framework;
using UniCorn.Utils;
using UnityEngine;

namespace UniCorn.Tests.Editor.Utils
{
    public class Vector2ExtensionTest
    {
        [TestCase(0, 0, 0, 0, 0, ExpectedResult = true)]
        [TestCase(0, 1, 0, 0, 1, ExpectedResult = true)]
        [TestCase(1, 0, 1, 0, 0, ExpectedResult = true)]
        [TestCase(1, 1, 1, 0, 1, ExpectedResult = true)]
        public bool X0ZTest(params float[] elements)
        {
            Assert.AreEqual(elements.Length, 5);
            Vector2 input = new Vector2(elements[0], elements[1]);
            Vector3 expectedOutput = new Vector3(elements[2], elements[3], elements[4]);
            return input.X0Z() == expectedOutput;
        }
    }
}
