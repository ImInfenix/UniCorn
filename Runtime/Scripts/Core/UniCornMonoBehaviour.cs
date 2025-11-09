using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNICORN_FOR_ZENJECT
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UniCorn.Zenject"), InternalsVisibleTo("UniCorn.Tests")]
#endif

namespace UniCorn.Core
{
#if UNICORN_FOR_ZENJECT
    internal class UniCornMonoBehaviour : MonoBehaviour, ICoroutineHandler
#else
    public class UniCornMonoBehaviour : MonoBehaviour, ICoroutineHandler
#endif
    {
#if !UNICORN_FOR_ZENJECT
        public static UniCornMonoBehaviour Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject();
                    _instance = gameObject.AddComponent<UniCornMonoBehaviour>();

                    DontDestroyOnLoad(gameObject);
                }

                return _instance;
            }
        }

        private static UniCornMonoBehaviour _instance;
#endif

        private readonly Dictionary<ulong, Coroutine> _pendingCoroutines = new();

        private ulong _requestCount;

        public CoroutineCancellationToken RunCoroutineFromUniCorn(IEnumerator enumerator)
        {
            CoroutineCancellationToken cancellationToken = new CoroutineCancellationToken(_requestCount++);
            _pendingCoroutines.Add(cancellationToken.CoroutineId,
                StartCoroutine(InternalCoroutine(enumerator, cancellationToken)));
            return cancellationToken;
        }

        public bool CancelCoroutine(CoroutineCancellationToken cancellationToken)
        {
            if (!_pendingCoroutines.TryGetValue(cancellationToken.CoroutineId, out Coroutine coroutineToCancel))
            {
                return false;
            }

            StopCoroutine(coroutineToCancel);
            _pendingCoroutines.Remove(cancellationToken.CoroutineId);
            return true;
        }

        private IEnumerator InternalCoroutine(IEnumerator coroutineToRun, CoroutineCancellationToken cancellationToken)
        {
            yield return coroutineToRun;
            _pendingCoroutines.Remove(cancellationToken.CoroutineId);
        }
    }
}
