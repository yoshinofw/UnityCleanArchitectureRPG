using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class DiscardItemUseCase
    {
        private IEventPublisher _eventPublisher;
        private Inventory _inventory;

        public DiscardItemUseCase(IEventPublisher eventPublisher, Inventory inventory)
        {
            _eventPublisher = eventPublisher;
            _inventory = inventory;
        }

        public (string ConfigId, int Count) Execute(int index)
        {
            SlotSet storageSlotSet = _inventory["Storage"];
            Item item = storageSlotSet[index];
            (string, int) result = (item.ConfigId, item.Count);
            storageSlotSet.RemoveItem(index);
            _eventPublisher.PostAll(_inventory.Events);
            return result;
        }
    }
}