using System;
using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Utils
{
	public class ListExtensionTest
	{
		private readonly List<int> _nullList = null;
		private readonly List<int> _emptyList = new();
		private readonly List<int> _oneItemList = new() {5};
		private readonly List<int> _multipleItemList = new() {5, 2, 8};

		[Test]
		public void IsEmptyNullList()
		{
			Assert.Throws<NullReferenceException>(() => _nullList.IsEmpty());
		}

		[Test]
		public void IsEmptyEmptyList()
		{
			Assert.IsTrue(_emptyList.IsEmpty());
		}

		[Test]
		public void IsEmptyNonEmptyList()
		{
			Assert.IsFalse(_oneItemList.IsEmpty());
		}

		[Test]
		public void GetLastNullList()
		{
			Assert.Throws<NullReferenceException>(() => _nullList.GetLast());
		}

		[Test]
		public void GetLastEmptyList()
		{
			List<int> list = new();
			Assert.Throws<ArgumentOutOfRangeException>(() => _emptyList.GetLast());
		}

		[Test]
		public void GetLastOneItemList()
		{
			Assert.AreEqual(5, _oneItemList.GetLast());
		}

		[Test]
		public void GetLastMultipleItemsList()
		{
			Assert.AreEqual(8, _multipleItemList.GetLast());
		}

		[Test]
		public void GetLastOrDefaultNullList()
		{
			Assert.Throws<NullReferenceException>(() => _nullList.GetLastOrDefault());
		}

		[Test]
		public void GetLastOrDefaultEmptyList()
		{
			Assert.AreEqual(0, _emptyList.GetLastOrDefault());
		}

		[Test]
		public void GetLastOrDefaultOneItemList()
		{
			Assert.AreEqual(5, _oneItemList.GetLastOrDefault());
		}

		[Test]
		public void GetLastOrDefaultMultipleItemsList()
		{
			Assert.AreEqual(8, _multipleItemList.GetLastOrDefault());
		}
	}
}
