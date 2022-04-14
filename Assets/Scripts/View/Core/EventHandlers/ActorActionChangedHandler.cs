using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.ActorDomain.Entities;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.View.Item;

namespace UCARPG.View.Core
{
    public class ActorActionPerformedHandler
    {
        private ItemConfigOverView _itemConfigOverView;
        private ActorPresenter _actorPresenter;
        private InventoryController _inventoryController;
        private StatController _statController;
        private SceneItemManager _sceneItemManager;

        public ActorActionPerformedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<ActorActionPerformed>(HandleEvent);
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _inventoryController = gameSystemProvider.InventoryController;
            _statController = gameSystemProvider.StatController;
            _sceneItemManager = gameSystemProvider.SceneItemManager;
        }

        private void HandleEvent(ActorActionPerformed e)
        {
            _actorPresenter.OnActorActionPerformed(e.Id, e.Action);
            switch (e.Action)
            {
                case "Cast":
                    _statController.ModifyStatValue(e.StatIdsByType["Mana"], -(_itemConfigOverView[e.MagicConfigId] as MagicConfig).ManaCost);
                    break;
                case "Pickup":
                    SceneItem sceneItem = _sceneItemManager.SelectedSceneItem;
                    _sceneItemManager.Remove(sceneItem);
                    ItemConfig itemConfig = _itemConfigOverView[sceneItem.ConfigId];
                    _inventoryController.StoreItem(itemConfig.Id, itemConfig.Type, itemConfig.Capacity, sceneItem.Count);
                    sceneItem.OnPickuped();
                    break;
            }
        }
    }
}