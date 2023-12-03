﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Utils
{
	public class ListExtensionTest
	{
		[TestCase(typeof(NullReferenceException))]
		public void IsEmptyNullListTest(Type type)
		{
			Assert.Throws(type, () => ((List<int>) null).IsEmpty());
		}

		[TestCase(ExpectedResult = true)]
		[TestCase(5, ExpectedResult = false)]
		[TestCase(5, 2, ExpectedResult = false)]
		[TestCase(5, 2, 8, ExpectedResult = false)]
		public bool IsEmptyTest(params int[] listContent)
		{
			return new List<int>(listContent).IsEmpty();
		}

		[TestCase(false, typeof(NullReferenceException))]
		[TestCase(true, typeof(ArgumentOutOfRangeException))]
		public void GetLastNullListTest(bool initialize, Type type)
		{
			List<int> list = null;

			if (initialize)
			{
				list = new();
			}

			Assert.Throws(type, () => list.GetLast());
		}

		[TestCase(5, ExpectedResult = 5)]
		[TestCase(5, 2, ExpectedResult = 2)]
		[TestCase(5, 2, 8, ExpectedResult = 8)]
		public int GetLastTest(params int[] listContent)
		{
			return new List<int>(listContent).GetLast();
		}

		[TestCase(typeof(NullReferenceException))]
		public void GetLastOrDefaultNullListTest(Type type)
		{
			Assert.Throws(type, () => ((List<int>) null).GetLastOrDefault());
		}

		[TestCase(ExpectedResult = default(int))]
		[TestCase(5, ExpectedResult = 5)]
		[TestCase(5, 2, ExpectedResult = 2)]
		[TestCase(5, 2, 8, ExpectedResult = 8)]
		public int GetLastOrDefaultTest(params int[] listContent)
		{
			return new List<int>(listContent).GetLastOrDefault();
		}
	}
}
