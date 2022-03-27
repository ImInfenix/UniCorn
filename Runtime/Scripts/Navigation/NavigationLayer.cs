using System.Collections.Generic;
using UniCorn.Core;

namespace UniCorn.Navigation
{
    public class NavigationLayer
    {
        private readonly List<InteractableItem> _registeredItems = new();

        public IReadOnlyList<InteractableItem> RegisteredItems => _registeredItems;

        public AbstractLayout AssociatedLayout { get; }

        public NavigationLayer(AbstractLayout associatedLayout)
        {
            AssociatedLayout = associatedLayout;
        }

        public void Register(InteractableItem itemToRegister)
        {
            _registeredItems.Add(itemToRegister);
        }

        public void Unregister(InteractableItem itemToRegister)
        {
            _registeredItems.Remove(itemToRegister);
        }
    }
}
