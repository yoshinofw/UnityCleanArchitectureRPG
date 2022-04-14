using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class CanStoreItemUseCase
    {
        private Inventory _inventory;

        public CanStoreItemUseCase(Inventory inventory)
        {
            _inventory = inventory;
        }

        public bool Execute(string configId, int count)
        {
            return _inventory["Storage"].CanAccommodateNewItem(configId, count);
        }
    }
}