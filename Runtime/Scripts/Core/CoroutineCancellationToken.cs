namespace UniCorn.Core
{
    public class CoroutineCancellationToken
    {
        public ulong CoroutineId { get; }

        public CoroutineCancellationToken(ulong coroutineId)
        {
            CoroutineId = coroutineId;
        }
    }
}
