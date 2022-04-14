using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.ActorDomain.Entities;
using UCARPG.Domain.StatDomain.InterfaceAdapters;

namespace UCARPG.View.Core
{
    public class ActorCreatedHandler
    {
        private ActorConfigOverView _actorConfigOverView;
        private ItemConfigOverView _itemConfigOverView;
        private ActorPresenter _actorPresenter;
        private StatController _statController;

        public ActorCreatedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<ActorCreated>(HandleEvent);
            _actorConfigOverView = gameSystemProvider.ActorConfigOverView;
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _statController = gameSystemProvider.StatController;
        }

        private void HandleEvent(ActorCreated e)
        {
            ActorConfig actorConfig = _actorConfigOverView[e.ConfigId];
            WeaponConfig weaponConfig = _itemConfigOverView[e.WeaponConfigId] as WeaponConfig;
            _actorPresenter.OnActorCreated(e.Id, actorConfig.Prefab, weaponConfig.RuntimeAnimatorController, weaponConfig.Prefab);
            foreach (var statConfig in actorConfig.StatConfigOverView.All)
            {
                _statController.CreateStat(e.Id, statConfig.Type, statConfig.MaxValue);
            }
        }
    }
}