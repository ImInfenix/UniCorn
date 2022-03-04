using System.Collections;
using NUnit.Framework;
using UniCorn.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace UniCorn.Tests
{
    public class UniCornMonoBehaviourTest
    {
        const int ITERATIONS_COUNT = 5;

        private UniCornMonoBehaviour _uniCornMonoBehaviour;

        private int _counter;

        [SetUp]
        public void Setup()
        {
            _uniCornMonoBehaviour = new GameObject("UniCornMonoBehaviour").AddComponent<UniCornMonoBehaviour>();
            _counter = 0;
        }

        [TearDown]
        public void TearDown()
        {
            _uniCornMonoBehaviour.StopAllCoroutines();
            Object.Destroy(_uniCornMonoBehaviour.gameObject);
        }

        [UnityTest]
        public IEnumerator ExecuteCoroutineTest()
        {
            _uniCornMonoBehaviour.RunCoroutineFromUniCorn(SimpleCounter(ITERATIONS_COUNT));

            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                yield return null;
            }

            Assert.AreEqual(ITERATIONS_COUNT, _counter);
        }

        [UnityTest]
        public IEnumerator CancelCoroutineTest()
        {
            CoroutineCancellationToken cancellationToken = _uniCornMonoBehaviour.RunCoroutineFromUniCorn(SimpleCounter(ITERATIONS_COUNT));

            yield return null;

            Assert.AreEqual(true, _uniCornMonoBehaviour.CancelCoroutine(cancellationToken));

            for (int i = 0; i < ITERATIONS_COUNT - 1; i++)
            {
                yield return null;
            }

            Assert.AreEqual(1, _counter);
        }

        [UnityTest]
        public IEnumerator CancelMultipleTimesCoroutineTest()
        {
            CoroutineCancellationToken cancellationToken = _uniCornMonoBehaviour.RunCoroutineFromUniCorn(SimpleCounter(3));

            yield return null;

            Assert.AreEqual(true, _uniCornMonoBehaviour.CancelCoroutine(cancellationToken));

            yield return null;

            Assert.AreEqual(false, _uniCornMonoBehaviour.CancelCoroutine(cancellationToken));
            
            yield return null;
            
            Assert.AreEqual(1, _counter);
        }

        [UnityTest]
        public IEnumerator DontCancelTerminatedCoroutinesTest()
        {
            CoroutineCancellationToken cancellationToken = _uniCornMonoBehaviour.RunCoroutineFromUniCorn(SimpleCounter(3));

            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                yield return null;
            }

            Assert.AreEqual(false, _uniCornMonoBehaviour.CancelCoroutine(cancellationToken));
        }

        IEnumerator SimpleCounter(int iterationsCount)
        {
            for (int i = 0; i < iterationsCount; i++)
            {
                yield return null;
                _counter++;
            }
        }
    }
}
