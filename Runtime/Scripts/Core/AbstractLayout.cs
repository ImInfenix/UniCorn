using UnityEngine;

namespace UniCorn.Core
{
	public abstract class AbstractLayout : MonoBehaviour
	{
		public virtual bool DoesForwardInputsToLowerLayers => false;

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
