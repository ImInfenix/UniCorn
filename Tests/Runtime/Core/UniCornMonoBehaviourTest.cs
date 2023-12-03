using System.Collections;
using NUnit.Framework;
using UniCorn.Core;
using UnityEngine;
using UnityEngine.TestTools;
#if UNICORN_FOR_ZENJECT
using Zenject;
#else
using UniCorn.Standalone;
#endif

namespace UniCorn.Tests.Core
{
#if UNICORN_FOR_ZENJECT
    [TestFixture]
    public class UniCornMonoBehaviourTest : ZenjectUnitTestFixture
#else
    public class UniCornMonoBehaviourTest
#endif
    {
        const int ITERATIONS_COUNT = 5;

#if UNICORN_FOR_ZENJECT
        [Inject] private ICoroutineHandler _coroutineHandler;
#else
        private ICoroutineHandler _coroutineHandler;
#endif

        private int _counter;

        [SetUp]
        public void SetupTests()
        {
            _counter = 0;

#if UNICORN_FOR_ZENJECT
            Container.BindInterfacesAndSelfTo<UniCornMonoBehaviour>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Inject(this);
#else
            _coroutineHandler = new GameObject().AddComponent<UniCornMonoBehaviour>();
#endif
        }

        [UnityTest]
        public IEnumerator ExecuteCoroutineTest()
        {
            _coroutineHandler.RunCoroutineFromUniCorn(SimpleCounter(ITERATIONS_COUNT));

            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                yield return null;
            }

            Assert.AreEqual(ITERATIONS_COUNT, _counter);
        }

        [UnityTest]
        public IEnumerator CancelCoroutineTest()
        {
            CoroutineCancellationToken cancellationToken = _coroutineHandler.RunCoroutineFromUniCorn(SimpleCounter(ITERATIONS_COUNT));

            yield return null;

            Assert.AreEqual(true, _coroutineHandler.CancelCoroutine(cancellationToken));

            for (int i = 0; i < ITERATIONS_COUNT - 1; i++)
            {
                yield return null;
            }

            Assert.AreEqual(1, _counter);
        }

        [UnityTest]
        public IEnumerator CancelMultipleTimesCoroutineTest()
        {
            CoroutineCancellationToken cancellationToken = _coroutineHandler.RunCoroutineFromUniCorn(SimpleCounter(3));

            yield return null;

            Assert.AreEqual(true, _coroutineHandler.CancelCoroutine(cancellationToken));

            yield return null;

            Assert.AreEqual(false, _coroutineHandler.CancelCoroutine(cancellationToken));

            yield return null;

            Assert.AreEqual(1, _counter);
        }

        [UnityTest]
        public IEnumerator DontCancelTerminatedCoroutinesTest()
        {
            CoroutineCancellationToken cancellationToken = _coroutineHandler.RunCoroutineFromUniCorn(SimpleCounter(3));

            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                yield return null;
            }

            Assert.AreEqual(false, _coroutineHandler.CancelCoroutine(cancellationToken));
        }

        private IEnumerator SimpleCounter(int iterationsCount)
        {
            for (int i = 0; i < iterationsCount; i++)
            {
                yield return null;
                _counter++;
            }
        }
    }
}
