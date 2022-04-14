using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.View.ActorComponent;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class ActorRevivedHandler
    {
        private ActorConfigOverView _actorConfigOverView;
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private StatController _statController;
        private InventoryUIController _inventoryUIController;

        public ActorRevivedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<PlayerActorRevived>(HandleEvent);
            _actorConfigOverView = gameSystemProvider.ActorConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _statController = gameSystemProvider.StatController;
            _inventoryUIController = gameSystemProvider.InventoryUIController;
        }

        private void HandleEvent(PlayerActorRevived e)
        {
            string id = _actorPresenter.PlayerActorId;
            string configId = _actorController.GetActorConfigId(id);
            foreach (var statConfig in _actorConfigOverView[configId].StatConfigOverView.All)
            {
                string statId = _actorController.GetActorStatId(id, statConfig.Type);
                _statController.ModifyStatValue(statId, statConfig.MaxValue);
            }
            _inventoryUIController.CanOpen = true;
        }
    }
}