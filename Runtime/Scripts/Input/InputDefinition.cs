using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UniCorn.Input
{
    [CreateAssetMenu(fileName = "InputActionMap", menuName = "UniCorn/Input/InputActionMap")]
    public class InputDefinition : ScriptableObject
    {
        [SerializeField] private InputActionReference[] _buttonsActions;
        [SerializeField] private InputActionReference[] _oneAxisActions;
        [SerializeField] private InputActionReference[] _twoAxisActions;

        public IReadOnlyList<InputActionReference> ButtonsActions => _buttonsActions;
        public IReadOnlyList<InputActionReference> OneAxisActions => _oneAxisActions;
        public IReadOnlyList<InputActionReference> TwoAxisActions => _twoAxisActions;
    }
}
