using UniCorn.Input;
using UnityEngine;

namespace UniCorn.Core
{
    public class UniCornSetup : MonoBehaviour
    {
        private UniCornSetup _instance;

        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning($"Multiple instance of UniCornSetup detected. Destroying {gameObject.name}");
                return;
            }

            DontDestroyOnLoad(gameObject);

            Setup();
        }

        private void Setup()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
        }
    }
}
