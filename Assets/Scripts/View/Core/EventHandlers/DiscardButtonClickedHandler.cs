using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.View.Item;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class DiscardButtonClickedHandler
    {
        private ActorPresenter _actorPresenter;
        private InventoryController _inventoryController;
        private SceneItemManager _sceneItemManager;

        public DiscardButtonClickedHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<DiscardButtonClicked>(HandleEvent);
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _inventoryController = gameSystemProvider.InventoryController;
            _sceneItemManager = gameSystemProvider.SceneItemManager;
        }

        private void HandleEvent(DiscardButtonClicked e)
        {
            (string ConfigId, int Count) item = _inventoryController.DiscardItem(e.Index);
            _sceneItemManager.Create(_actorPresenter[_actorPresenter.PlayerActorId].transform.position, item.ConfigId, item.Count);
        }
    }
}