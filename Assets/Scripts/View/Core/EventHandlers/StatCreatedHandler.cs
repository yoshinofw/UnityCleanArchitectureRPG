using UnityEngine;
using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.Entities;

namespace UCARPG.View.Core
{
    public class StatCreatedHandler
    {
        private ActorConfigOverView _actorConfigOverView;
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private StatPresenter _statPresenter;

        public StatCreatedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<StatCreated>(HandleEvent);
            _actorConfigOverView = gameSystemProvider.ActorConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _statPresenter = gameSystemProvider.StatPresenter;
        }

        private void HandleEvent(StatCreated e)
        {
            _actorController.CommitActorStatId(e.ActorId, e.Type, e.Id);
            GameObject prefab = _actorConfigOverView[_actorController.GetActorConfigId(e.ActorId)].StatConfigOverView[e.Type].Prefab;
            _statPresenter.OnStatCreated(e.Id, e.MaxValue, prefab, _actorPresenter[e.ActorId]);
        }
    }
}