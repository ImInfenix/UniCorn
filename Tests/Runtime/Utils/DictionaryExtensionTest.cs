using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Utils
{
	public class DictionaryExtensionTest
	{
		[Test]
		public void AddIfDoesntExistEmptyDictionary()
		{
			Dictionary<int, int> dictionary = new();
			dictionary.AddIfDoesntExist(0, 5);
			Assert.AreEqual(5, dictionary[0]);
			Assert.IsTrue(dictionary.Count == 1);
		}

		[Test]
		public void AddIfDoesntExistAlreadyExisting()
		{
			Dictionary<int, int> dictionary = new() { { 0, 5 } };
			dictionary.AddIfDoesntExist(0, 10);
			Assert.AreEqual(5, dictionary[0]);
			Assert.IsTrue(dictionary.Count == 1);
		}

		[Test]
		public void RemoveAndRetrieveIfExistsEmptyDictionary()
		{
			Dictionary<int, int> dictionary = new();
			Assert.IsFalse(dictionary.RemoveAndRetrieveIfExists(0, out int retrievedValue));
			Assert.AreEqual(default(int), retrievedValue);
			Assert.IsTrue(dictionary.Count == 0);
		}

		[Test]
		public void RemoveAndRetrieveIfExistsAlreadyExisting()
		{
			Dictionary<int, int> dictionary = new() { { 0, 5 } };
			Assert.IsTrue(dictionary.Count == 1);
			Assert.IsTrue(dictionary.RemoveAndRetrieveIfExists(0, out int retrievedValue));
			Assert.AreEqual(5, retrievedValue);
			Assert.IsTrue(dictionary.Count == 0);
		}

		[Test]
		public void GetAndAddIfDoesntExistEmptyDictionary()
		{
			Dictionary<int, int> dictionary = new();
			Assert.IsTrue(dictionary.Count == 0);
			Assert.AreEqual(default(int), dictionary.GetAndAddIfDoesntExist(0));
			Assert.IsTrue(dictionary.Count == 1);
		}

		[Test]
		public void GetAndAddIfDoesntExistAlreadyExisting()
		{
			Dictionary<int, int> dictionary = new() { { 0, 5 } };
			Assert.IsTrue(dictionary.Count == 1);
			Assert.AreEqual(5, dictionary.GetAndAddIfDoesntExist(0));
			Assert.IsTrue(dictionary.Count == 1);
		}
	}
}
