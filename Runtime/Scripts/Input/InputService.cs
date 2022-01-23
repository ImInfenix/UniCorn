using System.Collections.Generic;
using UniCorn.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Input
{
    public class InputService : IService
    {
        private readonly InputDefinition _inputDefinition;

        private readonly Dictionary<InputAction, InputReader> _intentBindings = new Dictionary<InputAction, InputReader>();

        public InputService(InputDefinition inputDefinition)
        {
            _inputDefinition = inputDefinition;
        }

        public void Initialize()
        {
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            List<InputActionReference> actions = new List<InputActionReference>();

            actions.AddRange(_inputDefinition.ButtonsActions);
            actions.AddRange(_inputDefinition.OneAxisActions);
            actions.AddRange(_inputDefinition.TwoAxisActions);

            foreach (InputActionReference inputActionReference in actions)
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
            InputAction currentAction = context.action;

            if (_intentBindings.ContainsKey(currentAction))
            {
                Debug.Log(_intentBindings[currentAction].GetValue(context));
            }
        }

        public void Dispose()
        {

        }
    }
}
