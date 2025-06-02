using NUnit.Framework;
using UniCorn.Utils;
using UnityEngine;

namespace UniCorn.Tests.Editor.Utils
{
    public class Vector3ExtensionTest
    {
        [TestCase(0, 0.5f, 0, 0, 0, ExpectedResult = true)]
        [TestCase(0, 0.5f, 1, 0, 1, ExpectedResult = true)]
        [TestCase(1, 0.5f, 0, 1, 0, ExpectedResult = true)]
        [TestCase(1, 0.5f, 1, 1, 1, ExpectedResult = true)]
        public bool XZTest(params float[] elements)
        {
            Assert.AreEqual(elements.Length, 5);
            Vector3 input = new Vector3(elements[0], elements[1], elements[2]);
            Vector2 expectedOutput = new Vector2(elements[3], elements[4]);
            return input.XZ() == expectedOutput;
        }
    }
}
