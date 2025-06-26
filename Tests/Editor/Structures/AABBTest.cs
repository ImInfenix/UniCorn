using NUnit.Framework;
using UniCorn.Structures;
using UnityEngine;

namespace UniCorn.Tests.Editor.Structures
{
    public class AABBTest
    {
        [Test]
        public void SizeZero()
        {
            AABB aabb = new AABB() { Min = Vector2.zero, Max = Vector2.zero };
            Assert.AreEqual(Vector2.zero, aabb.Center);
            Assert.AreEqual(Vector2.zero, aabb.Size);
        }

        [Test]
        public void SizeOne()
        {
            AABB aabb = new AABB() { Min = Vector2.zero, Max = Vector2.one };
            Assert.AreEqual(Vector2.one / 2f, aabb.Center);
            Assert.AreEqual(Vector2.one, aabb.Size);
        }

        [Test]
        public void SizeMinusOne()
        {
            AABB aabb = new AABB() { Min = Vector2.zero, Max = Vector2.one * -1f };
            Assert.AreEqual(Vector2.one / 2f * -1f, aabb.Center);
            Assert.AreEqual(Vector2.one * -1f, aabb.Size);
        }
    }
}
