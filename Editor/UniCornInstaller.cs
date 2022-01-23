using System;
using System.Text;
using UniCorn.Editor.Utils;
using UnityEditor;
using UnityEditor.Build;

namespace UniCorn.Editor
{
    public static class UniCornInstaller
    {
        private const string ZENJECT_INSTALLER_MENU_ITEM_PATH = "UniCorn/Install For Zenject";

        private const string PROMPT_WINDOWS_TITLE = "UniCorn Installer";

        private const string PROMPT_INSTALL_CONFIRM_MESSAGE = "Do you want to install UniCorn for Zenject?\n\nThis will add " +
                                                              UniCornEditorConstants.UNICORN_FOR_ZENJECT_DEFINE_SYMBOL_NAME +
                                                              " to your project symbols.";

        private const string PROMPT_UNINSTALL_CONFIRM_MESSAGE = "Do you want to uninstall UniCorn for Zenject?\n\nThis will remove " +
                                                                UniCornEditorConstants.UNICORN_FOR_ZENJECT_DEFINE_SYMBOL_NAME +
                                                                " from your project symbols.";

        private static readonly NamedBuildTarget _currentBuildTarget = NamedBuildTarget.Standalone;

        [MenuItem(ZENJECT_INSTALLER_MENU_ITEM_PATH)]
        public static void InstallUniCornForZenject()
        {
            if (IsZenjectForUniCornInstalled())
            {
                DoIfUserAccept(PROMPT_UNINSTALL_CONFIRM_MESSAGE,
                    () => ScriptingSymbolsUtils.RemoveScriptingSymbol(UniCornEditorConstants.UNICORN_FOR_ZENJECT_DEFINE_SYMBOL_NAME, _currentBuildTarget),
                    () => EditorUtility.DisplayDialog(PROMPT_WINDOWS_TITLE, "UniCorn for Zenject has been uninstalled.", UniCornEditorConstants.PROMPT_OK)
                );
            }
            else
            {
                DoIfUserAccept(PROMPT_INSTALL_CONFIRM_MESSAGE,
                    () => ScriptingSymbolsUtils.AddScriptingSymbol(UniCornEditorConstants.UNICORN_FOR_ZENJECT_DEFINE_SYMBOL_NAME, _currentBuildTarget),
                    () => EditorUtility.DisplayDialog(PROMPT_WINDOWS_TITLE, "UniCorn for Zenject has now installed.", UniCornEditorConstants.PROMPT_OK)
                );
            }

            InstallUniCornForZenjectValidate();
        }

        [MenuItem(ZENJECT_INSTALLER_MENU_ITEM_PATH, true)]
        public static bool InstallUniCornForZenjectValidate()
        {
            Menu.SetChecked(ZENJECT_INSTALLER_MENU_ITEM_PATH, IsZenjectForUniCornInstalled());
            return true;
        }

        private static bool IsZenjectForUniCornInstalled()
        {
            return PlayerSettings.GetScriptingDefineSymbols(_currentBuildTarget).Contains(UniCornEditorConstants.UNICORN_FOR_ZENJECT_DEFINE_SYMBOL_NAME);
        }

        private static void DoIfUserAccept(string promptMessage, Action action, Action continueWith = null)
        {
            if (EditorUtility.DisplayDialog(PROMPT_WINDOWS_TITLE, promptMessage, UniCornEditorConstants.PROMPT_YES, UniCornEditorConstants.PROMPT_NO))
            {
                continueWith?.Invoke();
            }
        }
    }
}
