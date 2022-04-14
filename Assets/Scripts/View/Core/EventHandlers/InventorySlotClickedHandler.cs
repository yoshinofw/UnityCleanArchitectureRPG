using System;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class InventorySlotClickedHandler
    {
        private InventoryController _inventoryController;
        private InventoryUIController _inventoryUIController;

        public InventorySlotClickedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<InventorySlotClicked>(HandleEvent);
            _inventoryController = gameSystemProvider.InventoryController;
            _inventoryUIController = gameSystemProvider.InventoryUIController;
        }

        private void HandleEvent(InventorySlotClicked e)
        {
            if (!_inventoryController.IsExistItem(e.SetName, e.Index))
            {
                return;
            }
            switch (e.SetName)
            {
                case "Weapon":
                case "Magic":
                case "Consumable":
                    _inventoryController.UnequipItem(e.SetName, e.Index);
                    break;
                case "Storage":
                    _inventoryUIController.OpenStorageItemOptionMenu(e.Index, _inventoryController.CanEquipItemFromStorage(e.Index));
                    break;
                default:
                    throw new Exception("Not match any case");
            }
        }
    }
}