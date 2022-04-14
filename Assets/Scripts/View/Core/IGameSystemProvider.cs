using UCARPG.ScriptableObjects;
using UCARPG.Utilities;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.View.Camera;
using UCARPG.View.Input;
using UCARPG.View.Item;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public interface IGameSystemProvider
    {
        ActorConfigOverView ActorConfigOverView { get; }
        ItemConfigOverView ItemConfigOverView { get; }
        EventBus EventBus { get; }
        ActorController ActorController { get; }
        ActorPresenter ActorPresenter { get; }
        InventoryController InventoryController { get; }
        StatController StatController { get; }
        StatPresenter StatPresenter { get; }
        PlayerInputReceiver PlayerInputReceiver { get; }
        CameraController CameraController { get; }
        DamageDisPlayer DamageDisPlayer { get; }
        SceneItemManager SceneItemManager { get; }
        GameMenuUIController GameMenuUIController { get; }
        InventoryUIController InventoryUIController { get; }
        ConsumableHotbarUIController ConsumableHotbarUIController { get; }
        PickupItemTipUIController PickupItemTipUIController { get; }
    }
}