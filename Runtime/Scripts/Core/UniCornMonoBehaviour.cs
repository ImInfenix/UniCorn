using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniCorn.Core
{
    public class UniCornMonoBehaviour : MonoBehaviour
    {
        private readonly Dictionary<ulong, Coroutine> _pendingCoroutines = new();

        private ulong _requestCount;

        public CoroutineCancellationToken RunCoroutineFromUniCorn(IEnumerator enumerator)
        {
            CoroutineCancellationToken cancellationToken = new CoroutineCancellationToken(_requestCount++);
            _pendingCoroutines.Add(cancellationToken.CoroutineId, StartCoroutine(InternalCoroutine(enumerator, cancellationToken)));
            return cancellationToken;
        }

        public bool CancelCoroutine(CoroutineCancellationToken cancellationToken)
        {
            if (_pendingCoroutines.TryGetValue(cancellationToken.CoroutineId, out Coroutine coroutineToCancel))
            {
                StopCoroutine(coroutineToCancel);
                _pendingCoroutines.Remove(cancellationToken.CoroutineId);
                return true;
            }

            return false;
        }

        public IEnumerator InternalCoroutine(IEnumerator coroutineToRun, CoroutineCancellationToken cancellationToken)
        {
            yield return coroutineToRun;
            _pendingCoroutines.Remove(cancellationToken.CoroutineId);
        }
    }
}
