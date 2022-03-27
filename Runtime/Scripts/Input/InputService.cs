using System.Collections.Generic;
using UniCorn.Core;
using UniCorn.Navigation;
using UniCorn.Utils;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UniCorn.Input
{
    public class InputService : AbstractAsynchronouslyLoadedService
    {
        private readonly IInputActionCollection _inputActionCollection;
        private readonly NavigationService _navigationService;

        private readonly Dictionary<InputAction, InputDefinition> _inputActionToInputDefinition = new();

        public InputService(IInputActionCollection inputActionCollection, NavigationService navigationService)
        {
            _inputActionCollection = inputActionCollection;
            _navigationService = navigationService;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (InputAction inputAction in _inputActionCollection)
            {
                inputAction.started += OnInputAction;
                inputAction.performed += OnInputAction;
                inputAction.canceled += OnInputAction;

                inputAction.Enable();
            }

            LoadInputDefinitions();
        }

        public override void Dispose()
        {
            base.Initialize();

            foreach (InputAction inputAction in _inputActionCollection)
            {
                inputAction.performed -= OnInputAction;
                inputAction.canceled -= OnInputAction;
            }
        }

        private void LoadInputDefinitions()
        {
            if (!AddressablesUtils.TryGetResourceLocation(UniCornAddressableKeys.UNICORN_ADDRESSABLE_INPUT_DEFINITIONS_KEY,
                    typeof(InputDefinition), out IList<IResourceLocation> resourceLocation))
            {
                return;
            }

            AsyncOperationHandle<IList<InputDefinition>> loadInputDefinitionsHandle = Addressables.LoadAssetsAsync<InputDefinition>(resourceLocation, null);

            loadInputDefinitionsHandle.Completed += OnInputDefinitionsLoaded;

            RegisterAsynchronousOperation(loadInputDefinitionsHandle);
        }

        private void OnInputDefinitionsLoaded(AsyncOperationHandle<IList<InputDefinition>> operationHandle)
        {
            foreach (InputDefinition inputDefinition in operationHandle.Result)
            {
                _inputActionToInputDefinition.Add(inputDefinition.InputActionReference.action, inputDefinition);
            }
        }

        protected override void OnAsyncOperationsCompleted()
        {
        }

        private void OnInputAction(InputAction.CallbackContext callbackContext)
        {
            _navigationService.OnInputAction(callbackContext, _inputActionToInputDefinition[callbackContext.action]);
        }
    }
}
