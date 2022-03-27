using System;
using System.Collections.Generic;
using UniCorn.Core;
using UniCorn.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UniCorn.Navigation
{
    [RequireComponent(typeof(Selectable))]
    public class InteractableItem : MonoBehaviour
    {
        [SerializeField] private InputDefinition[] _inputDefinitions;

        private InputService _inputService;
        private NavigationService _navigationService;

        public Action onItemPressed;
        public Action onItemReleased;

        private AbstractLayout _parentLayout;
        private Selectable _selectable;

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
            _inputService = inputService;
            _navigationService = navigationService;
        }

        private void Awake()
        {
            _parentLayout = GetComponentInParent<AbstractLayout>();
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
            
            Register();
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
    }
}
