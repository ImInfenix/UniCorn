using System;
using System.Collections.Generic;
using NUnit.Framework;
using UniCorn.Utils;

namespace UniCorn.Tests.Utils
{
    public class DictionaryExtensionTest
    {
        [Test]
        public void AddIfDoesntExistNullDictionary()
        {
            Dictionary<int, int> dictionary = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<NullReferenceException>(() => dictionary.AddIfDoesntExist(0, 0));
        }

        [Test]
        public void AddIfDoesntExistEmptyDictionary()
        {
            Dictionary<int, int> dictionary = new();
            dictionary.AddIfDoesntExist(0, 0);
            Assert.AreEqual(0, dictionary[0]);
        }

        [Test]
        public void AddIfDoesntExistAlreadyExisting()
        {
            Dictionary<int, int> dictionary = new() { { 0, 5 } };
            dictionary.AddIfDoesntExist(0, 0);
            Assert.AreNotEqual(0, dictionary[0]);
        }
    }
}
