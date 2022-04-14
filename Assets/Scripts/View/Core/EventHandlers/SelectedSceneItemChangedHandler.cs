using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.View.Item;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class SelectedSceneItemChangedHandler
    {
        private InventoryController _inventoryController;
        private PickupItemTipUIController _pickupItemTipUIController;

        public SelectedSceneItemChangedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<SelectedSceneItemChanged>(HandleEvent);
            _inventoryController = gameSystemProvider.InventoryController;
            _pickupItemTipUIController = gameSystemProvider.PickupItemTipUIController;
        }

        private void HandleEvent(SelectedSceneItemChanged e)
        {
            if (e.SceneItem != null)
            {
                _pickupItemTipUIController.Show(_inventoryController.CanStoreItem(e.SceneItem.ConfigId, e.SceneItem.Count));
            }
            else
            {
                _pickupItemTipUIController.Hide();
            }
        }
    }
}