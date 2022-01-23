using UnityEditor;
using UnityEditor.Build;

namespace UniCorn.Editor.Utils
{
    public static class ScriptingSymbolsUtils
    {
        public static void AddScriptingSymbol(string symbolToAdd, NamedBuildTarget buildTarget)
        {
            string definedSymbols = PlayerSettings.GetScriptingDefineSymbols(buildTarget);
            if (definedSymbols.Contains(symbolToAdd))
            {
                return;
            }

            definedSymbols = $"{definedSymbols};{symbolToAdd}";
            PlayerSettings.SetScriptingDefineSymbols(buildTarget, definedSymbols);
        }

        public static void RemoveScriptingSymbol(string symbolToRemove, NamedBuildTarget buildTarget)
        {
            string definedSymbols = PlayerSettings.GetScriptingDefineSymbols(buildTarget);
            if (!definedSymbols.Contains(symbolToRemove))
            {
                return;
            }

            definedSymbols = definedSymbols.Replace($";{symbolToRemove}", "");
            definedSymbols = definedSymbols.Replace($"{symbolToRemove};", "");
            definedSymbols = definedSymbols.Replace($"{symbolToRemove}", "");
            PlayerSettings.SetScriptingDefineSymbols(buildTarget, definedSymbols);
        }
    }
}
