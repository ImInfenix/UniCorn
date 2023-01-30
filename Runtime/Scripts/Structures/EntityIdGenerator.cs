namespace UniCorn.Structures
{
	public class EntityIdGenerator
	{
		private uint _currentId;

		public uint NextId => _currentId++;
	}
}
