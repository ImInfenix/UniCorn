using System;
using System.Collections.Generic;
using UniCorn.Core;
using UniCorn.Input;
using UnityEngine;
using UnityEngine.UI;
#if UNICORN_FOR_ZENJECT
using Zenject;
#endif

namespace UniCorn.Navigation
{
	[RequireComponent(typeof(Selectable))]
	public class InteractableItem : MonoBehaviour
	{
		[SerializeField] private AbstractLayout _parentLayout;
		[SerializeField] private InputDefinition[] _inputDefinitions;

		private NavigationService _navigationService;

		public Action onItemPressed;
		public Action onItemReleased;

		private Selectable _selectable;
		private InitializationStatus _initializationStatus;

		public IEnumerable<InputDefinition> InputDefinitions => _inputDefinitions;

		public Selectable Selectable
		{
			get
			{
				if (_selectable == null)
				{
					_selectable = GetComponent<Selectable>();
				}

				return _selectable;
			}
		}

#if UNICORN_FOR_ZENJECT
		[Inject]
#endif
		public void InitializeDependencies(InputService inputService, NavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		private void Start()
		{
			AttemptToInitialize(InitializationStatus.Started);
		}

		private void OnEnable()
		{
			switch (Selectable)
			{
				case Button button:
					button.onClick.AddListener(OnItemReleased);
					break;
				case Toggle toggle:
					toggle.onValueChanged.AddListener(OnToggleValueChanged);
					break;
			}

			AttemptToInitialize(InitializationStatus.EnabledOnce);
		}

		private void OnDisable()
		{
			switch (Selectable)
			{
				case Button button:
					button.onClick.RemoveListener(OnItemReleased);
					break;
				case Toggle toggle:
					toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
					break;
			}

			Unregister();
		}

		private void AttemptToInitialize(InitializationStatus newStatus)
		{
			const InitializationStatus requiredInitializationStatus = InitializationStatus.Started | InitializationStatus.EnabledOnce;
			_initializationStatus |= newStatus;

			if (_initializationStatus != requiredInitializationStatus)
			{
				return;
			}

			if (_parentLayout == null)
			{
				_parentLayout = GetComponentInParent<AbstractLayout>();
			}

			if (gameObject.activeInHierarchy)
			{
				Register();
			}
		}

		private void Register()
		{
			_navigationService.Register(this, _parentLayout);
		}

		private void Unregister()
		{
			_navigationService.Unregister(this, _parentLayout);
		}

		public bool DoesListenToInputDefinition(InputDefinition inputDefinition)
		{
			foreach (InputDefinition listenedInputDefinition in InputDefinitions)
			{
				if (listenedInputDefinition == inputDefinition)
				{
					return true;
				}
			}

			return false;
		}

		public void OnItemPressed()
		{
			onItemPressed?.Invoke();
		}

		public void OnItemReleased()
		{
			onItemReleased?.Invoke();
		}

		private void OnToggleValueChanged(bool isToggled)
		{
			OnItemPressed();
			OnItemReleased();
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_parentLayout = GetComponentInParent<AbstractLayout>();
		}
#endif
	}
}
