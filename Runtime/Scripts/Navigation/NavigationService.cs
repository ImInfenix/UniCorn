using System;
using System.Collections.Generic;
using UniCorn.Core;
using UniCorn.Input;
using UniCorn.Logging;
using UniCorn.Utils;
using UnityEngine.InputSystem;

namespace UniCorn.Navigation
{
	public partial class NavigationService : ILoggedService
	{
		private readonly Dictionary<string, InputDefinition> _registeredInputDefinitions = new();
		private readonly List<NavigationLayer> _layersList = new();

		public void Initialize()
		{
		}

		public void Dispose()
		{
		}

		public void Register(InteractableItem itemToRegister, AbstractLayout layoutToRegisterOn)
		{
			AddLayerIfDoesntExist(layoutToRegisterOn);

			NavigationLayer layerToRegisterOn = GetLayer(layoutToRegisterOn);
			layerToRegisterOn.Register(itemToRegister);

			foreach (InputDefinition inputDefinition in itemToRegister.InputDefinitions)
			{
				_registeredInputDefinitions.AddIfDoesntExist(inputDefinition.name, inputDefinition);
			}
		}

		public void Unregister(InteractableItem itemToRegister, AbstractLayout layoutToUnregisterFrom)
		{
			NavigationLayer layerToRegisterOn = GetLayer(layoutToUnregisterFrom);

			if (layerToRegisterOn == null)
			{
				return;
			}

			layerToRegisterOn.Unregister(itemToRegister);

			if (layerToRegisterOn.RegisteredItems.Count == 0)
			{
				RemoveLayerIfExists(layoutToUnregisterFrom);
			}
		}

		public bool OnInputAction(InputAction.CallbackContext callbackContext, InputDefinition inputDefinition)
		{
			if (!OnInputActionInternal(callbackContext, inputDefinition, out Action actionToExecute))
			{
				return false;
			}

			actionToExecute.Invoke();
			return true;
		}

		private bool OnInputActionInternal(InputAction.CallbackContext callbackContext, InputDefinition inputDefinition, out Action actionToExecute)
		{
			return OnInputActionInternal(callbackContext, inputDefinition, out actionToExecute, _layersList.Count - 1);
		}

		private bool OnInputActionInternal(InputAction.CallbackContext callbackContext, InputDefinition inputDefinition, out Action actionToExecute, int layerIndex)
		{
			actionToExecute = null;

			NavigationLayer navigationLayer = _layersList[layerIndex];
			foreach (InteractableItem interactableItem in navigationLayer.RegisteredItems )
			{
				if (!interactableItem.DoesListenToInputDefinition(inputDefinition))
				{
					continue;
				}

				bool consumedInput = false;

				switch (callbackContext.phase)
				{
					case InputActionPhase.Started:
						actionToExecute = interactableItem.OnItemPressed;
						consumedInput = interactableItem.onItemPressed != null;
						break;
					case InputActionPhase.Performed:
						break;
					case InputActionPhase.Canceled:
						actionToExecute = interactableItem.OnItemReleased;
						consumedInput = interactableItem.onItemReleased != null;
						break;
				}

				if (consumedInput)
				{
					return true;
				}
			}

			if (layerIndex > 0 && navigationLayer.AssociatedLayout.DoesForwardInputsToLowerLayers)
			{
				return OnInputActionInternal(callbackContext, inputDefinition, out actionToExecute, layerIndex - 1);
			}

			return false;
		}

		private void AddLayerIfDoesntExist(AbstractLayout layoutToAdd)
		{
			if (DoesLayerExist(layoutToAdd))
			{
				return;
			}

			_layersList.Add(new NavigationLayer(layoutToAdd));
		}

		private void RemoveLayerIfExists(AbstractLayout layoutToRemove)
		{
			if (!DoesLayerExist(layoutToRemove))
			{
				return;
			}

			_layersList.RemoveAt(GetLayerIndex(layoutToRemove));
		}

		private bool DoesLayerExist(AbstractLayout associatedLayout)
		{
			return GetLayer(associatedLayout) != null;
		}

		private NavigationLayer GetLayer(AbstractLayout layoutAssociatedToLayer)
		{
			int layerIndex = GetLayerIndex(layoutAssociatedToLayer);

			if (layerIndex < 0)
			{
				return null;
			}

			return _layersList[layerIndex];
		}

		private int GetLayerIndex(AbstractLayout layoutAssociatedToLayer)
		{
			for (int i = _layersList.Count - 1; i >= 0; i--)
			{
				if (_layersList[i].AssociatedLayout == layoutAssociatedToLayer)
				{
					return i;
				}
			}

			return -1;
		}
	}
}
