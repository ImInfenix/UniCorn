using NUnit.Framework;
using UniCorn.Utils;
using UnityEngine;

namespace UniCorn.Tests.Utils
{
    public class Vector2IntExtensionTest
    {
        [TestCase(0, 0, 0, 0, 0, ExpectedResult = true)]
        [TestCase(0, 1, 0, 0, 1, ExpectedResult = true)]
        [TestCase(1, 0, 1, 0, 0, ExpectedResult = true)]
        [TestCase(1, 1, 1, 0, 1, ExpectedResult = true)]
        public bool X0ZTest(params int[] elements)
        {
            Assert.AreEqual(elements.Length, 5);
            Vector2Int input = new Vector2Int(elements[0], elements[1]);
            Vector3Int expectedOutput = new Vector3Int(elements[2], elements[3], elements[4]);
            return input.X0Z() == expectedOutput;
        }
    }
}
