using System.Collections.Generic;
using UniCorn.Utils;

namespace UniCorn.Navigation
{
	public partial class NavigationService
	{
		public string[] GetLogContent()
		{
			List<string> logs = new();
			foreach (NavigationLayer layer in _layersList)
			{
				logs.Add($"{LoggingUtils.ToVariableDisplayName("Layer", layer.AssociatedLayout)}");
				foreach (InteractableItem interactableItem in layer.RegisteredItems)
				{
					logs.Add($"\t{LoggingUtils.ToVariableDisplayName("Item", interactableItem)}");
				}
			}

			return logs.ToArray();
		}
	}
}
