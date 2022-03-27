using System.Collections.Generic;
using UniCorn.Input;
using UniCorn.Utils;
using UnityEngine.InputSystem;

namespace UniCorn.Navigation
{
    public class NavigationService
    {
        private readonly List<InteractableItem> _registeredItems = new();
        private readonly Dictionary<string, InputDefinition> _registeredInputDefinitions = new();

        public void Register(InteractableItem interactableItem)
        {
            _registeredItems.Add(interactableItem);

            foreach (InputDefinition inputDefinition in interactableItem.InputDefinitions)
            {
                _registeredInputDefinitions.AddIfDoesntExist(inputDefinition.name, inputDefinition);
            }
        }

        public void Unregister(InteractableItem interactableItem)
        {
            _registeredItems.Remove(interactableItem);
        }

        public bool OnInputAction(InputAction.CallbackContext callbackContext, InputDefinition inputDefinition)
        {
            foreach (InteractableItem interactableItem in _layersList.GetLastOrDefault().RegisteredItems)
            {
                if (!interactableItem.DoesListenToInputDefinition(inputDefinition))
                {
                    continue;
                }

                switch (callbackContext.phase)
                {
                    case InputActionPhase.Started:
                        interactableItem.OnItemPressed();
                        break;
                    case InputActionPhase.Performed:
                        break;
                    case InputActionPhase.Canceled:
                        interactableItem.OnItemReleased();
                        break;
                }

                return true;
            }

            return false;
        }
            }
        }
    }
}
