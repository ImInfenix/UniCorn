using System.Collections.Generic;
using UnityEditor;

namespace UniCorn.Editor.AssetPostProcessing
{
	public class ScenesWindowAssetPostProcessor : AssetPostprocessor
	{
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			if (!EditorWindow.HasOpenInstances<ScenesWindow.ScenesWindow>())
			{
				return;
			}

			List<string[]> arraysToTest = new List<string[]> {importedAssets, deletedAssets, movedAssets, movedFromAssetPaths};

			bool shouldUpdate = false;
			foreach (string[] updatedAssets in arraysToTest)
			{
				foreach (string assetPath in updatedAssets)
				{
					if (!ScenesWindow.ScenesWindow.ShouldSceneBeDisplayed(assetPath))
					{
						continue;
					}

					shouldUpdate = true;
					break;
				}

				if (shouldUpdate)
				{
					break;
				}
			}

			if (shouldUpdate)
			{
				ScenesWindow.ScenesWindow.UpdateWindowContent();
			}
		}
	}
}
