using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Editor.Utils
{
    public class FloatUtilsTest
    {
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(0, 1, ExpectedResult = false)]
        [TestCase(0, FloatUtils.PRECISION * 0.1f, ExpectedResult = true)]
        [TestCase(0.3f, 0.1f + 0.2f, ExpectedResult = true)]
        [TestCase(0.3f, 0.3000005f, ExpectedResult = true)]
        public bool AreAlmostEqualTest(params float[] elements)
        {
            Assert.AreEqual(2, elements.Length);
            return FloatUtils.AreAlmostEqual(elements[0], elements[1]);
        }
    }
}
