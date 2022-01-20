using System;
using UnityEngine.InputSystem;

namespace UniCorn.Input
{
    public class InputReader
    {
        private readonly Type _inputInputType;

        public InputReader(Type inputType)
        {
            _inputInputType = inputType;
        }

        public Input GetValue(InputAction.CallbackContext callbackContext)
        {
            InputStatus inputStatus = InputStatus.Undefined;

            if (callbackContext.performed)
            {
                inputStatus = InputStatus.Performed;
            }
            else if (callbackContext.canceled)
            {
                inputStatus = InputStatus.Canceled;
            }
            else if (callbackContext.started)
            {
                inputStatus = InputStatus.Started;
            }

            return new Input(_inputInputType, callbackContext.ReadValueAsObject(), inputStatus);
        }
    }
}
