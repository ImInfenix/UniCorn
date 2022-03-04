namespace UniCorn.Core
{
    public class CoroutineCancellationToken
    {
        public uint CoroutineId { get; }

        public CoroutineCancellationToken(uint coroutineId)
        {
            CoroutineId = coroutineId;
        }
    }
}
