using System;
using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class StoreItemUseCase
    {
        private IEventPublisher _eventPublisher;
        private Inventory _inventory;

        public StoreItemUseCase(IEventPublisher eventPublisher, Inventory inventory)
        {
            _eventPublisher = eventPublisher;
            _inventory = inventory;
        }

        public void Execute(string configId, string type, int capacity, int count)
        {
            SlotSet storageSlotSet = _inventory["Storage"];
            if (!storageSlotSet.CanAccommodateNewItem(configId, count))
            {
                throw new Exception("Sotre item failed");
            }
            Item item = new Item(configId, type, capacity, count);
            storageSlotSet.StackItem(ref item);
            storageSlotSet.AddItemToNullSlot(item);
            _eventPublisher.PostAll(_inventory.Events);
        }
    }
}