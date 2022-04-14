using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.View.ActorComponent;

namespace UCARPG.View.Core
{
    public class ActorSpawnerExecutedHandler
    {
        ActorConfigOverView _actorConfigOverView;
        ActorController _actorController;
        ActorPresenter _actorPresenter;

        public ActorSpawnerExecutedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<ActorSpawnerExecuted>(HandleEvent);
            _actorConfigOverView = gameSystemProvider.ActorConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
        }

        private void HandleEvent(ActorSpawnerExecuted e)
        {
            _actorPresenter.OnActorSpawnerExecuted(e.Position, e.Rotation, e.PatrolPath);
            _actorController.CreateActor(e.ConfigId, _actorConfigOverView[e.ConfigId].DefaultWeaponConfigId);
        }
    }
}