using System;
using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class EquipItemFromStorageUseCase
    {
        private IEventPublisher _eventPublisher;
        private Inventory _inventory;

        public EquipItemFromStorageUseCase(IEventPublisher eventPublisher, Inventory inventory)
        {
            _eventPublisher = eventPublisher;
            _inventory = inventory;
        }

        public void Execute(int index)
        {
            SlotSet storageSlotSet = _inventory["Storage"];
            Item item = storageSlotSet[index];
            SlotSet equipmentSlotSet = _inventory[item.Type];
            if (!equipmentSlotSet.AddItemToNullSlot(item))
            {
                throw new Exception("Equip item failed");
            }
            storageSlotSet.RemoveItem(index);
            _eventPublisher.PostAll(_inventory.Events);
        }
    }
}