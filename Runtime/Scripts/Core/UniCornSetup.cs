using System;
using System.Collections.Generic;
using UniCorn.Localization;
using UniCorn.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Core
{
	public class UniCornSetup : MonoBehaviour
	{
		[SerializeField] private IInputActionCollection _inputActionCollection;
		[SerializeField] private LocalizationSettings _localizationSettings;

		public static UniCornSetup Instance { get; private set; }

		private ICoroutineHandler _coroutineHandler;
		private Dictionary<Type, IService> _initializedServices = new();

		public ICoroutineHandler GetCoroutineHandler()
		{
			return _coroutineHandler;
		}

		public T GetService<T>() where T : IService
		{
			return (T) _initializedServices[typeof(T)];
		}

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				Debug.LogWarning($"Multiple instance of UniCornSetup detected. Destroying {gameObject.name}");
				return;
			}

			DontDestroyOnLoad(gameObject);

			Instance = this;
			Setup();
		}

		private void Setup()
		{
			_coroutineHandler = gameObject.AddComponent<UniCornMonoBehaviour>();
			InitializeServices();
		}

		private void InitializeServices()
		{
			InitializeSingleService<NavigationService>();
			// TODO initialize input service from user's code inherited type
			//InitializeSingleService<InputService>(_inputActionCollection, GetService<NavigationService>());
			InitializeSingleService<TranslationService>(_localizationSettings, _coroutineHandler);
			InitializeSingleService<LocalizationSystemService>(_localizationSettings, GetService<TranslationService>());
		}

		private void InitializeSingleService<T>(params object[] args) where T : IService
		{
			_initializedServices[typeof(T)] = (IService) Activator.CreateInstance(typeof(T), args);
		}
	}
}
