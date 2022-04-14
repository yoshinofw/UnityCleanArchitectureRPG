using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.View.Input;
using UCARPG.View.Item;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class ActionInputTriggeredHandler
    {
        private ItemConfigOverView _itemConfigOverView;
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private InventoryController _inventoryController;
        private StatController _statController;
        private SceneItemManager _sceneItemManager;
        private ConsumableHotbarUIController _consumableHotbarUIController;

        public ActionInputTriggeredHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<ActionInputTriggered>(HandleEvent);
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _inventoryController = gameSystemProvider.InventoryController;
            _statController = gameSystemProvider.StatController;
            _sceneItemManager = gameSystemProvider.SceneItemManager;
            _consumableHotbarUIController = gameSystemProvider.ConsumableHotbarUIController;
        }

        private void HandleEvent(ActionInputTriggered e)
        {
            string actorId = _actorPresenter.PlayerActorId;
            switch (e.Action)
            {
                case "Cast":
                    if (!CanCast(actorId))
                    {
                        return;
                    }
                    break;
                case "Pickup":
                    SceneItem sceneItem = _sceneItemManager.SelectedSceneItem;
                    if (sceneItem == null || !_inventoryController.CanStoreItem(sceneItem.ConfigId, sceneItem.Count))
                    {
                        return;
                    }
                    break;
                case "Use":
                    if (!_inventoryController.IsExistItem("Consumable", _consumableHotbarUIController.SelectedIndex))
                    {
                        return;
                    }
                    break;
            }
            _actorController.MakeActorPerformAction(actorId, e.Action);
        }

        private bool CanCast(string actorId)
        {
            string magicConfigId = _actorController.GetActorMagicConfigId(actorId);
            if (string.IsNullOrEmpty(magicConfigId))
            {
                return false;
            }
            float manaCost = (_itemConfigOverView[magicConfigId] as MagicConfig).ManaCost;
            if (_statController.GetStatValue(_actorController.GetActorStatId(actorId, "Mana")) < manaCost)
            {
                return false;
            }
            return true;
        }
    }
}