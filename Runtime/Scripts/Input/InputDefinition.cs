using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Input
{
    [CreateAssetMenu(fileName = "InputDefinition", menuName = "UniCorn/InputDefinition")]
    public class InputDefinition : ScriptableObject
    {
        [SerializeField] private InputActionReference _inputActionReference;

        public InputActionReference InputActionReference => _inputActionReference;
    }
}
