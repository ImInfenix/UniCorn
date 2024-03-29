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
		public static readonly InteractableItemInitializationComparer REGISTER_COMPARER = new();
		public static readonly InteractableItemInitializationReversedComparer UNREGISTER_COMPARER = new();

		[SerializeField] private AbstractLayout _parentLayout;
		[SerializeField] private InputDefinition[] _inputDefinitions;

		[Tooltip("Defines in which order interactable items are (un)registered on the same frame. Lowest is earliest.")]
		[SerializeField]
		private int _initializationPriority;

		private NavigationService _navigationService;

		public Action onItemPressed;
		public Action onItemReleased;

		private Selectable _selectable;
		private InitializationStatus _initializationStatus;

		public IEnumerable<InputDefinition> InputDefinitions => _inputDefinitions;
		public AbstractLayout ParentLayout => _parentLayout;

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
			const InitializationStatus requiredInitializationStatus =
				InitializationStatus.Started | InitializationStatus.EnabledOnce;
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
			_navigationService.Register(this);
		}

		private void Unregister()
		{
			_navigationService.Unregister(this);
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
			if (_parentLayout == null)
			{
				_parentLayout = GetComponentInParent<AbstractLayout>();
			}
		}
#endif

		public class InteractableItemInitializationComparer : IComparer<InteractableItem>
		{
			public virtual int Compare(InteractableItem first, InteractableItem second)
			{
				if (first is null)
				{
					if (second is null)
					{
						return 0;
					}

					return -1;
				}

				return second is null ? 1 : first._initializationPriority.CompareTo(second._initializationPriority);
			}
		}

		public class InteractableItemInitializationReversedComparer : InteractableItemInitializationComparer
		{
			public override int Compare(InteractableItem first, InteractableItem second)
			{
				return base.Compare(second, first);
			}
		}
	}
}
