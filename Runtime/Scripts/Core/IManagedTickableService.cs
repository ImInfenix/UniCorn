namespace UniCorn.Core
{
	public interface IManagedTickableService : IService
	{
		public void Tick(float deltaTime);
	}
}
