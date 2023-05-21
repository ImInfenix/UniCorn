namespace UniCorn.Structures
{
	public abstract class IdGenerator<T>
	{
		protected T _currentId;

		public void Reset() => _currentId = default;
	}

	public class IdGeneratorUInt : IdGenerator<uint>
	{
		public uint NextId => _currentId++;
	}

	public class IdGeneratorInt : IdGenerator<int>
	{
		public int NextId => _currentId++;
	}
}
