using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;

namespace UCARPG.Domain.InventoryDomain.UseCases
{
    public class UseConsumableUseCase
    {
        private IEventPublisher _eventPublisher;
        private Inventory _inventory;

        public UseConsumableUseCase(IEventPublisher eventPublisher, Inventory inventory)
        {
            _eventPublisher = eventPublisher;
            _inventory = inventory;
        }

        public void Execute(int index)
        {
            _inventory["Consumable"].DecreaseItem(index, 1);
            _eventPublisher.PostAll(_inventory.Events);
        }
    }
}