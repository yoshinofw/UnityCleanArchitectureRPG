using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.View.ActorComponent;
using UCARPG.View.Camera;

namespace UCARPG.View.Core
{
    public class PlayerActorCreatedHandler
    {
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private InventoryController _inventoryController;
        private CameraController _cameraController;

        public PlayerActorCreatedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<PlayerActorCreated>(HandleEvent);
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _inventoryController = gameSystemProvider.InventoryController;
            _cameraController = gameSystemProvider.CameraController;
        }

        private void HandleEvent(PlayerActorCreated e)
        {
            _actorPresenter.OnPlayerActorCreated(e.ViewObject);
            string weaponConfigId = _actorController.GetActorWeaponConfigId(_actorPresenter.PlayerActorId);
            if (weaponConfigId != "Fist")
            {
                _inventoryController.EquipNewItem(weaponConfigId, "Weapon", 1, 1);
            }
            _cameraController.FollowTarget = e.ViewObject.transform;
        }
    }
}