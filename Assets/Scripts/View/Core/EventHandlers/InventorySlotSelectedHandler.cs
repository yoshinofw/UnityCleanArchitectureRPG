using UCARPG.ScriptableObjects;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class InventorySlotSelectedHandler
    {
        private ItemConfigOverView _itemConfigOverView;
        private InventoryController _inventoryController;
        private InventoryUIController _inventoryUIController;

        public InventorySlotSelectedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<InventorySlotSelected>(HandleEvent);
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _inventoryController = gameSystemProvider.InventoryController;
            _inventoryUIController = gameSystemProvider.InventoryUIController;
        }

        private void HandleEvent(InventorySlotSelected e)
        {
            if (!_inventoryController.IsExistItem(e.SetName, e.Index))
            {
                _inventoryUIController.HideItemDescription();
                return;
            }
            string itemConfigId = _inventoryController.GetItemConfigId(e.SetName, e.Index);
            string context = _itemConfigOverView[itemConfigId].GetDescription();
            _inventoryUIController.ShowItemDescription(e.SetName, e.Index, context);
        }
    }
}