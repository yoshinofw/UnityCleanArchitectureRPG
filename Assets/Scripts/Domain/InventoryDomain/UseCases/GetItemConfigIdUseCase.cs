using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class GetItemConfigIdUseCase
    {
        private Inventory _inventory;

        public GetItemConfigIdUseCase(Inventory inventory)
        {
            _inventory = inventory;
        }

        public string Execute(string setName, int index)
        {
            return _inventory[setName][index].ConfigId;
        }
    }
}