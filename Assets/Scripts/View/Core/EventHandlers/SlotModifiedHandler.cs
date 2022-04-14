using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.Entities;
using UCARPG.View.Item;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class SlotModifiedHandler
    {
        private ItemConfigOverView _itemConfigOverView;
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private InventoryController _inventoryController;
        private SceneItemManager _sceneItemManager;
        private InventoryUIController _inventoryUIController;
        private PickupItemTipUIController _pickupItemTipUIController;

        public SlotModifiedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<ItemModified>(HandleEvent);
            gameSystemProvider.EventBus.Register<SlotCleared>(HandleEvent);
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _inventoryController = gameSystemProvider.InventoryController;
            _sceneItemManager = gameSystemProvider.SceneItemManager;
            _inventoryUIController = gameSystemProvider.InventoryUIController;
            _pickupItemTipUIController = gameSystemProvider.PickupItemTipUIController;
        }

        private void HandleEvent(ItemModified e)
        {
            _inventoryUIController.ModifySlot(e.SetName, e.Index, _itemConfigOverView[e.ItemConfigId].Icon, e.ItemCount.ToString());
            switch (e.SetName)
            {
                case "Storage":
                    UpdatePickupItemTipUI();
                    break;
                case "Weapon":
                    _actorController.ChangeActorWeapon(_actorPresenter.PlayerActorId, e.ItemConfigId);
                    break;
                case "Magic":
                    _actorController.ChangeActorMagic(_actorPresenter.PlayerActorId, e.ItemConfigId);
                    break;
            }
        }

        private void HandleEvent(SlotCleared e)
        {
            _inventoryUIController.ClearSlot(e.SetName, e.Index);
            switch (e.SetName)
            {
                case "Storage":
                    UpdatePickupItemTipUI();
                    break;
                case "Weapon":
                    _actorController.ChangeActorWeapon(_actorPresenter.PlayerActorId, "Fist");
                    break;
                case "Magic":
                    _actorController.ChangeActorMagic(_actorPresenter.PlayerActorId, string.Empty);
                    break;
            }
        }

        private void UpdatePickupItemTipUI()
        {
            if (_sceneItemManager.SelectedSceneItem != null)
            {
                SceneItem sceneItem = _sceneItemManager.SelectedSceneItem;
                _pickupItemTipUIController.Show(_inventoryController.CanStoreItem(sceneItem.ConfigId, sceneItem.Count));
            }
        }
    }
}