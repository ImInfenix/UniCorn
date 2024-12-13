using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Editor.Utils
{
	public class DictionaryExtensionTest
	{
		[TestCase(ExpectedResult = 0)]
		[TestCase(0, 5, ExpectedResult = 1)]
		[TestCase(0, 5, 0, 5, ExpectedResult = 1)]
		[TestCase(0, 5, 1, 5, ExpectedResult = 2)]
		public int AddIfDoesntExistTest(params int[] elements)
		{
			Assert.AreEqual(elements.Length % 2, 0);

			Dictionary<int, int> dictionary = new();
			for (int i = 0; i < elements.Length; i += 2)
			{
				bool wasValueAlreadyHere = dictionary.TryGetValue(elements[i], out int oldValue);
				dictionary.AddIfDoesntExist(elements[i], elements[i + 1]);

				Assert.AreEqual(dictionary[elements[i]], wasValueAlreadyHere ? oldValue : elements[i + 1]);
			}

			return dictionary.Count;
		}

		[TestCase(default(int), default(int), ExpectedResult = 0)]
		[TestCase(0, 5, 0, 5, ExpectedResult = 0)]
		[TestCase(0, 5, 2, default(int), ExpectedResult = 1)]
		[TestCase(0, 5, 1, 6, 0, 5, ExpectedResult = 1)]
		[TestCase(0, 5, 1, 6, 1, 6, ExpectedResult = 1)]
		[TestCase(0, 5, 1, 6, 2, default(int), ExpectedResult = 2)]
		public int RemoveAndRetrieveIfExistsTest(params int[] elements)
		{
			const int parameterArgumentCount = 2;
			Assert.IsTrue(elements.Length >= parameterArgumentCount);
			Assert.AreEqual(elements.Length % 2, parameterArgumentCount % 2);

			Dictionary<int, int> dictionary = new();
			for (int i = 0; i < elements.Length - parameterArgumentCount; i += 2)
			{
				dictionary[elements[i]] = elements[i + 1];
			}

			int previousElementCount = dictionary.Count;
			bool wasValueRetrieved = dictionary.RemoveAndRetrieveIfExists(elements[^2], out int retrievedValue);

			Assert.IsTrue(dictionary.Count == (wasValueRetrieved ? previousElementCount - 1 : previousElementCount));
			Assert.AreEqual(retrievedValue, elements[^1]);

			return dictionary.Count;
		}

		[TestCase(default(int), default(int), ExpectedResult = 1)]
		[TestCase(0, 5, 0, 5, ExpectedResult = 1)]
		[TestCase(0, 5, 2, default(int), ExpectedResult = 2)]
		[TestCase(0, 5, 1, 6, 0, 5, ExpectedResult = 2)]
		[TestCase(0, 5, 1, 6, 1, 6, ExpectedResult = 2)]
		[TestCase(0, 5, 1, 6, 2, default(int), ExpectedResult = 3)]
		public int GetAndAddIfDoesntExistTest(params int[] elements)
		{
			const int parameterArgumentCount = 2;
			Assert.IsTrue(elements.Length >= parameterArgumentCount);
			Assert.AreEqual(elements.Length % 2, parameterArgumentCount % 2);

			Dictionary<int, int> dictionary = new();
			for (int i = 0; i < elements.Length - parameterArgumentCount; i += 2)
			{
				dictionary[elements[i]] = elements[i + 1];
			}

			int retrievedValue = dictionary.GetAndAddIfDoesntExist(elements[^2]);
			Assert.AreEqual(retrievedValue, elements[^1]);
			return dictionary.Count;
		}
	}
}
