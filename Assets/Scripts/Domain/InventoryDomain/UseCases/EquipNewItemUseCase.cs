using System;
using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class EquipNewItemUseCase
    {
        private IEventPublisher _eventPublisher;
        private Inventory _inventory;

        public EquipNewItemUseCase(IEventPublisher eventPublisher, Inventory inventory)
        {
            _eventPublisher = eventPublisher;
            _inventory = inventory;
        }

        public void Execute(string configId, string type, int capacity, int count)
        {
            Item item = new Item(configId, type, capacity, count);
            if (!_inventory[type].AddItemToNullSlot(item))
            {
                throw new Exception("Equip item failed");
            }
            _eventPublisher.PostAll(_inventory.Events);
        }
    }
}