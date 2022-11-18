using System.Collections.Generic;
using UnityEngine;

namespace UniCorn.Logging
{
    public class ServiceLogger : MonoBehaviour
    {
        private static ServiceLogger _instance;

        private readonly List<LoggedServiceData> _servicesToLog = new();
        private bool _shouldBeLogging;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeInstance()
        {
            GameObject loggerGameObject = new GameObject(nameof(ServiceLogger));
            loggerGameObject.hideFlags = HideFlags.HideInHierarchy;
            _instance = loggerGameObject.AddComponent<ServiceLogger>();
            DontDestroyOnLoad(loggerGameObject);
        }

        public static void ToggleLogging()
        {
            _instance._shouldBeLogging = !_instance._shouldBeLogging;
        }

        public static void Register(ILoggedService service)
        {
            _instance._servicesToLog.Add(new LoggedServiceData(service));
        }

        public static void Unregister(ILoggedService service)
        {
            LoggedServiceData loggedServiceData = _instance._servicesToLog.Find(data => data.Service == service);
            _instance._servicesToLog.Remove(loggedServiceData);
        }

        private void OnGUI()
        {
            if (!_shouldBeLogging)
            {
                return;
            }

            Rect logWindowRect = new Rect(0, 0, Screen.width / 3f, 50);
            GUILayout.Window(GetHashCode(), logWindowRect, DrawLogWindow, "Service Logger");

            float logSelectionWindowHeight = _shouldSelectionWindowBeMinimized ? 50 : Screen.height / 3f;
            Rect logSelectionWindowRect = new Rect(Screen.width * 2f / 3f, 0, Screen.width / 3f, logSelectionWindowHeight);
            GUILayout.Window(GetHashCode() + 1, logSelectionWindowRect, DrawLogSelectionWindow, "Logged Services Selection");
        }

        private void DrawLogWindow(int id)
        {
            GUIStyle serviceNameStyle = new GUIStyle(GUI.skin.label)
            {
                normal = {textColor = Color.blue}
            };
            GUIStyle logEntryStyle = new GUIStyle(GUI.skin.label)
            {
                normal = {textColor = Color.yellow}
            };

            foreach (LoggedServiceData serviceData in _servicesToLog)
            {
                if (!serviceData.ShouldBeLogged)
                {
                    continue;
                }

                GUILayout.Label(serviceData.Service.GetServiceName(), serviceNameStyle);

                foreach (string logEntry in serviceData.Service.GetLogContent())
                {
                    GUILayout.Label($"\t{logEntry}", logEntryStyle);
                }
            }
        }

        private bool _shouldSelectionWindowBeMinimized;

        private void DrawLogSelectionWindow(int id)
        {
            DrawToggle(ref _shouldSelectionWindowBeMinimized, "Minimize");

            if (_shouldSelectionWindowBeMinimized)
            {
                return;
            }

            GUILayout.Space(10);

            for (var i = 0; i < _servicesToLog.Count; i++)
            {
                LoggedServiceData serviceData = _servicesToLog[i];
                DrawToggle(ref serviceData.ShouldBeLogged, serviceData.Service.GetServiceName());
                _servicesToLog[i] = serviceData;
            }
        }

        private void DrawToggle(ref bool value, string toggleName, GUIStyle style = null)
        {
            value = GUILayout.Toggle(value, toggleName, style ?? GUI.skin.toggle);
        }

        private struct LoggedServiceData
        {
            public ILoggedService Service { get; }
            public bool ShouldBeLogged;

            public LoggedServiceData(ILoggedService service)
            {
                Service = service;
                ShouldBeLogged = false;
            }
        }
    }
}
