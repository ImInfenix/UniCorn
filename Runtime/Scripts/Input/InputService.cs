using System;
using System.Collections.Generic;
using UniCorn.Core;
using UniCorn.Core.AsynchronousLoading;
using UniCorn.Navigation;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UniCorn.Input
{
    public class InputService : AbstractAsynchronouslyLoadedService, ITickableService
    {
        private readonly IInputActionCollection _inputActionCollection;
        private readonly NavigationService _navigationService;

        private readonly Dictionary<Guid, InputDefinition> _inputActionToInputDefinition = new();

        private bool _isInputEventConsumed;

        protected InputService(IInputActionCollection inputActionCollection, NavigationService navigationService)
        {
            _inputActionCollection = inputActionCollection;
            _navigationService = navigationService;
        }

        public override void Initialize()
        {
            foreach (InputAction inputAction in _inputActionCollection)
            {
                inputAction.started += OnInputAction;
                inputAction.performed += OnInputAction;
                inputAction.canceled += OnInputAction;

                inputAction.Enable();
            }

            LoadAssetsAsync<InputDefinition>(UniCornAddressableKeys.UNICORN_ADDRESSABLE_INPUT_DEFINITIONS_KEY, OnInputDefinitionsLoaded);
        }

        public override void Dispose()
        {
            foreach (InputAction inputAction in _inputActionCollection)
            {
                inputAction.performed -= OnInputAction;
                inputAction.canceled -= OnInputAction;
            }
        }

        private void OnInputDefinitionsLoaded(AsyncOperationHandle<IList<InputDefinition>> operationHandle)
        {
            foreach (InputDefinition inputDefinition in operationHandle.Result)
            {
                _inputActionToInputDefinition.Add(inputDefinition.InputActionReference.action.id, inputDefinition);
            }
        }

        private void OnInputAction(InputAction.CallbackContext callbackContext)
        {
            if (_isInputEventConsumed || !_inputActionToInputDefinition.TryGetValue(callbackContext.action.id, out InputDefinition inputDefinition))
            {
                return;
            }

            if (_navigationService.OnInputAction(callbackContext, inputDefinition))
            {
                _isInputEventConsumed = true;
            }
        }

        public void Tick()
        {
            _isInputEventConsumed = false;
        }
    }
}
