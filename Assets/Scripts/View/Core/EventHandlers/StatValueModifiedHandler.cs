using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.Entities;

namespace UCARPG.View.Core
{
    public class StatValueModifiedHandler
    {
        private ActorConfigOverView _actorConfigOverView;
        private ActorController _actorController;
        private StatController _statController;
        private StatPresenter _statPresenter;

        public StatValueModifiedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<StatValueModified>(HandleEvent);
            _actorConfigOverView = gameSystemProvider.ActorConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _statController = gameSystemProvider.StatController;
            _statPresenter = gameSystemProvider.StatPresenter;
        }

        private void HandleEvent(StatValueModified e)
        {
            _statPresenter.OnStatValueModified(e.Id, e.Value);
            if (e.Type == "Health" && e.Value == 0)
            {
                _actorController.MakeActorPerformAction(e.ActorId, "Death");
            }
            else if (e.Type == "Poise" && e.Value == 0)
            {
                _actorController.MakeActorPerformAction(e.ActorId, "GetHit");
                string actorConfigId = _actorController.GetActorConfigId(e.ActorId);
                _statController.ModifyStatValue(e.Id, _actorConfigOverView[actorConfigId].StatConfigOverView["Poise"].MaxValue);
            }
        }
    }
}