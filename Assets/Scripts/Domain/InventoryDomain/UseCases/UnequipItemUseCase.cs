using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class UnequipItemUseCase
    {
        private IEventPublisher _eventPublisher;
        private Inventory _inventory;

        public UnequipItemUseCase(IEventPublisher eventPublisher, Inventory inventory)
        {
            _eventPublisher = eventPublisher;
            _inventory = inventory;
        }

        public void Execute(string type, int index)
        {
            SlotSet equipmentSlotSet = _inventory[type];
            SlotSet storageSlotSet = _inventory["Storage"];
            Item item = equipmentSlotSet[index];
            if (!storageSlotSet.CanAccommodateNewItem(item.ConfigId, item.Count))
            {
                return;
            }
            storageSlotSet.StackItem(ref item);
            storageSlotSet.AddItemToNullSlot(item);
            equipmentSlotSet.RemoveItem(index);
            _eventPublisher.PostAll(_inventory.Events);
        }
    }
}