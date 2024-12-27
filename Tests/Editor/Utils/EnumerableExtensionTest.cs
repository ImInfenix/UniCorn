using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;
using Assert = UnityEngine.Assertions.Assert;

namespace UniCorn.Tests.Editor.Utils
{
    public class EnumerableExtensionTest
    {
        private class A
        {
        }

        private class B : A
        {
        }

        private class C : A
        {
        }

        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(1, 0, ExpectedResult = true)]
        [TestCase(3, 0, ExpectedResult = true)]
        [TestCase(0, 1, ExpectedResult = false)]
        [TestCase(0, 3, ExpectedResult = false)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(3, 3, ExpectedResult = true)]
        public bool AreAlmostEqualTest(params int[] elements)
        {
            Assert.AreEqual(2, elements.Length);
            List<A> list = new List<A>();
            for (int i = 0; i < elements[0]; i++)
            {
                list.Add(new B());
            }

            for (int i = 0; i < elements[1]; i++)
            {
                list.Add(new C());
            }

            return list.GetElementOfType<B, A>() != null;
        }
    }
}
