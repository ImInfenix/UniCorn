using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UniCorn
{
    public class ScenesWindow : EditorWindow
    {
        private static ScenesWindow _scenesWindow;

        private static string[] _existingScenesPaths;
        private static string[] _existingScenesNames;

        private static readonly Vector2Int WINDOW_MARGIN_VECTOR = new(5, 5);
        private static readonly Vector2Int BUTTON_SIZE = new(150, 20);

        private static Vector2 _scrollPosition;

        [MenuItem("UniCorn/Scenes Window", priority = 100)]
        public static void OpenWindow()
        {
            _scenesWindow = GetWindow<ScenesWindow>();
            _scenesWindow.titleContent = new GUIContent("Scenes Window");
            _scenesWindow.maximized = true;
            _scenesWindow.Show();

            UpdateWindowContent();
        }

        private static void UpdateWindowContent()
        {
            string[] existingScenesGuids = AssetDatabase.FindAssets($"t:scene", new[] {"Assets"});

            _existingScenesPaths = new string[existingScenesGuids.Length];
            _existingScenesNames = new string[existingScenesGuids.Length];

            for (int i = 0; i < existingScenesGuids.Length; i++)
            {
                string sceneGuid = existingScenesGuids[i];
                _existingScenesPaths[i] = AssetDatabase.GUIDToAssetPath(sceneGuid);
                _existingScenesNames[i] = Path.GetFileNameWithoutExtension(_existingScenesPaths[i]);
            }
        }

        private void OnGUI()
        {
            if (_scenesWindow == null)
            {
                _scenesWindow = GetWindow<ScenesWindow>();
            }

            if (_existingScenesNames == null)
            {
                UpdateWindowContent();

                if (_existingScenesNames == null)
                {
                    return;
                }
            }

            Rect windowContentSize = new Rect(WINDOW_MARGIN_VECTOR, _scenesWindow.position.size - WINDOW_MARGIN_VECTOR * 2);

            int maximumButtonPerRaw = Mathf.Max((int) _scenesWindow.position.width / (BUTTON_SIZE.x + WINDOW_MARGIN_VECTOR.x), 1);

            Vector2Int totalNumberOfButtons = new Vector2Int(maximumButtonPerRaw, _existingScenesNames.Length / maximumButtonPerRaw);
            Rect viewRect = new Rect(Vector2.zero, new Vector2(windowContentSize.width - WINDOW_MARGIN_VECTOR.x / 2, BUTTON_SIZE.y * totalNumberOfButtons.y + WINDOW_MARGIN_VECTOR.y * 2));

            _scrollPosition = GUI.BeginScrollView(windowContentSize, _scrollPosition, viewRect);

            for (int i = 0; i < _existingScenesNames.Length; i++)
            {
                Vector2 positionInGridAsCell = new Vector2Int(i % maximumButtonPerRaw, i / maximumButtonPerRaw);
                Vector2 positionInGrid = (BUTTON_SIZE + WINDOW_MARGIN_VECTOR) * positionInGridAsCell;

                float positionAvailableForButtons = viewRect.width - WINDOW_MARGIN_VECTOR.x * 2;
                float positionAvailablePerButton = positionAvailableForButtons / maximumButtonPerRaw;

                positionInGrid.x = WINDOW_MARGIN_VECTOR.x / 2
                                   + positionInGridAsCell.x * positionAvailablePerButton
                                   + (positionAvailablePerButton - BUTTON_SIZE.x) / 2;

                if (GUI.Button(new Rect(positionInGrid, BUTTON_SIZE), _existingScenesNames[i]))
                {
                    EditorSceneManager.OpenScene(_existingScenesPaths[i]);
                }
            }

            GUI.EndScrollView();
        }
    }
}
