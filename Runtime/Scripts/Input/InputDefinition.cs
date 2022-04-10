using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Input
{
    [CreateAssetMenu(fileName = nameof(InputDefinition), menuName = nameof(UniCorn) + "/" + nameof(InputDefinition))]
    public class InputDefinition : ScriptableObject
    {
        [SerializeField] private InputActionReference _inputActionReference;

        public InputActionReference InputActionReference => _inputActionReference;
    }
}
