using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class CanEquipItemFromStorageUseCase
    {
        private Inventory _inventory;

        public CanEquipItemFromStorageUseCase(Inventory inventory)
        {
            _inventory = inventory;
        }

        public bool Execute(int index)
        {
            Item item = _inventory["Storage"][index];
            return !_inventory[item.Type].IsAllSlotUsed;
        }
    }
}