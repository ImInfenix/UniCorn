#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UniCorn.Utils
{
    public static class UniCornApplication
    {
        public static void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
