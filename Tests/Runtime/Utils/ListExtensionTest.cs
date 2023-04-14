using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Utils
{
	public class ListExtensionTest
	{
		[Test]
		public void IsEmptyEmptyList()
		{
			List<int> list = new();
			Assert.IsTrue(list.IsEmpty());
		}

		[Test]
		public void IsEmptyNonEmptyList()
		{
			List<int> list = new() { 5 };
			Assert.IsFalse(list.IsEmpty());
		}

		[Test]
		public void GetLastOrDefaultEmptyList()
		{
			List<int> list = new();
			Assert.AreEqual(0, list.GetLastOrDefault());
		}

		[Test]
		public void GetLastOrDefaultOneItemList()
		{
			List<int> list = new() { 5 };
			Assert.AreEqual(5, list.GetLastOrDefault());
		}

		[Test]
		public void GetLastOrDefaultMultipleItemsList()
		{
			List<int> list = new() { 5, 2, 8 };
			Assert.AreEqual(8, list.GetLastOrDefault());
		}
	}
}
