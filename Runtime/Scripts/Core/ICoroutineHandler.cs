using System.Collections;

namespace UniCorn.Core
{
    public interface ICoroutineHandler
    {
        CoroutineCancellationToken RunCoroutineFromUniCorn(IEnumerator coroutine);
        bool CancelCoroutine(CoroutineCancellationToken cancellationToken);
    }
}
