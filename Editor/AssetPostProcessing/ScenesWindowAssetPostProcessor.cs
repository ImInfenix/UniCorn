using System.Linq;
using UnityEditor;

namespace UniCorn
{
    public class ScenesWindowAssetPostProcessor : AssetPostprocessor
    {
        private const string SCENE_FILES_EXTENSION = ".unity";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!EditorWindow.HasOpenInstances<ScenesWindow>())
            {
                return;
            }

            if (importedAssets.Any(assetPath => assetPath.EndsWith(SCENE_FILES_EXTENSION)) ||
                deletedAssets.Any(assetPath => assetPath.EndsWith(SCENE_FILES_EXTENSION)) ||
                movedAssets.Any(assetPath => assetPath.EndsWith(SCENE_FILES_EXTENSION)) ||
                movedFromAssetPaths.Any(assetPath => assetPath.EndsWith(SCENE_FILES_EXTENSION)))
            {
                ScenesWindow.UpdateWindowContent();
            }
        }
    }
}
