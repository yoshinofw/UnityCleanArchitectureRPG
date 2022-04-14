using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.View.ActorComponent;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class PlayerConsumableUsedHandler
    {
        private ItemConfigOverView _itemConfigOverView;
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private InventoryController _inventoryController;
        private StatController _statController;
        private ConsumableHotbarUIController _consumableHotbarUIController;

        public PlayerConsumableUsedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<PlayerConsumableUsed>(HandleEvent);
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _inventoryController = gameSystemProvider.InventoryController;
            _statController = gameSystemProvider.StatController;
            _consumableHotbarUIController = gameSystemProvider.ConsumableHotbarUIController;
        }

        private void HandleEvent(PlayerConsumableUsed e)
        {
            string itemConfigId = _inventoryController.GetItemConfigId("Consumable", _consumableHotbarUIController.SelectedIndex);
            ConsumableConfig consumableConfig = _itemConfigOverView[itemConfigId] as ConsumableConfig;
            string statId = _actorController.GetActorStatId(_actorPresenter.PlayerActorId, consumableConfig.ModifyStatType);
            _statController.ModifyStatValue(statId, consumableConfig.Modifier);
            _inventoryController.UseConsumable(_consumableHotbarUIController.SelectedIndex);
            _actorPresenter.PlayParticleSystem(_actorPresenter.PlayerActorId, consumableConfig.ModifyStatType);
        }
    }
}