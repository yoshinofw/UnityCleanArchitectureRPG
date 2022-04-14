using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class IsExistItemUseCase
    {
        private Inventory _inventory;

        public IsExistItemUseCase(Inventory inventory)
        {
            _inventory = inventory;
        }

        public bool Execute(string setName, int index)
        {
            return index < _inventory[setName].Count;
        }
    }
}