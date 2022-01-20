using System;
using System.Collections.Generic;
using UniCorn.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Input
{
    public class InputService : IService
    {
        private InputDefinition _inputDefinition;

        private readonly Dictionary<InputAction, InputReader> _intentBindings = new Dictionary<InputAction, InputReader>();

        public void Initialize()
        {
            InputDefinition[] inputDefinitions = Resources.LoadAll<InputDefinition>(GlobalConstants.UNICORN_RESOURCES_FOLDER_PATH);

            switch (inputDefinitions.Length)
            {
                case 0:
                    Debug.LogWarning($"No input definitions found in {GlobalConstants.UNICORN_RESOURCES_FOLDER_PATH} folder.");
                    return;
                case > 1:
                    Debug.LogError($"Multiple input definitions found in {GlobalConstants.UNICORN_RESOURCES_FOLDER_PATH} folder.");
                    break;
                default:
                    _inputDefinition = inputDefinitions[0];
                    break;
            }

            InitializeBindings();
        }

        private void InitializeBindings()
        {
            foreach (InputActionReference inputActionReference in _inputDefinition.ButtonsActions)
            {
                _intentBindings.Add(inputActionReference.action, new InputReader(typeof(bool)));

                inputActionReference.action.started += OnActionPerformed;
                inputActionReference.action.performed += OnActionPerformed;
                inputActionReference.action.canceled += OnActionPerformed;

                inputActionReference.action.Enable();
            }
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            if (_intentBindings.ContainsKey(context.action))
            {
                Debug.Log(_intentBindings[context.action].GetValue(context));
            }
        }

        public void Dispose()
        {

        }
    }
}
