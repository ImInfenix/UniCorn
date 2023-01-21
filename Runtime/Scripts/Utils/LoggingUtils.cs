using UnityEngine;

namespace UniCorn.Utils
{
    public static class LoggingUtils
    {
        public static string ToVariableDisplayName(string variableName, object variable) => $"{variableName}: {variable}";
        public static string ToVariableDisplayName(string variableName, Component variable)
        {
            return ToVariableDisplayName(variableName, variable ? variable.name : null);
        }
    }
}
