using UnityEngine;

namespace UniCorn.Core
{
	public abstract class AbstractLayout : MonoBehaviour
	{
		public virtual bool DoesForwardInputsToLowerLayers => false;

		public void Show()
		{
			gameObject.SetActive(true);

			OnShown();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
			OnHidden();
		}

		protected virtual void OnShown()
		{
		}

		protected virtual void OnHidden()
		{
		}
	}
}
