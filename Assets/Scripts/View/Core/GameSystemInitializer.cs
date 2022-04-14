using UnityEngine;
using UCARPG.ScriptableObjects;
using UCARPG.Utilities;
using UCARPG.Domain.Standard.InterfaceAdapters;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.ActorDomain.UseCases;
using UCARPG.Domain.ActorDomain.Entities;
using UCARPG.Domain.InventoryDomain.InterfaceAdapters;
using UCARPG.Domain.InventoryDomain.UseCases;
using UCARPG.Domain.InventoryDomain.Entities;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.UseCases;
using UCARPG.Domain.StatDomain.Entities;
using UCARPG.View.ActorComponent;
using UCARPG.View.Audio;
using UCARPG.View.Camera;
using UCARPG.View.Input;
using UCARPG.View.Item;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class GameSystemInitializer : MonoBehaviour, IGameSystemProvider
    {
        public ActorConfigOverView ActorConfigOverView { get => _actorConfigOverView; }
        public ItemConfigOverView ItemConfigOverView { get => _itemConfigOverView; }
        public EventBus EventBus { get; private set; }
        public ActorController ActorController { get; private set; }
        public ActorPresenter ActorPresenter { get => _actorPresenter; }
        public InventoryController InventoryController { get; private set; }
        public StatController StatController { get; private set; }
        public StatPresenter StatPresenter { get => _statPresenter; }
        public PlayerInputReceiver PlayerInputReceiver { get => _playerInputReceiver; }
        public CameraController CameraController { get => _cameraController; }
        public DamageDisPlayer DamageDisPlayer { get => _damageDisPlayer; }
        public SceneItemManager SceneItemManager { get => _sceneItemManager; }
        public GameMenuUIController GameMenuUIController { get => _gameMenuUIController; }
        public InventoryUIController InventoryUIController { get => _inventoryUIController; }
        public ConsumableHotbarUIController ConsumableHotbarUIController { get => _consumableHotbarUIController; }
        public PickupItemTipUIController PickupItemTipUIController { get => _pickupItemTipUIController; }
        [SerializeField]
        private ActorConfigOverView _actorConfigOverView;
        [SerializeField]
        private ItemConfigOverView _itemConfigOverView;
        [SerializeField]
        private EventBusProvider _eventBusProvider;
        [SerializeField]
        private ActorPresenter _actorPresenter;
        [SerializeField]
        private StatPresenter _statPresenter;
        [SerializeField]
        private PlayerInputReceiver _playerInputReceiver;
        [SerializeField]
        private CameraController _cameraController;
        [SerializeField]
        private DamageDisPlayer _damageDisPlayer;
        [SerializeField]
        private SceneItemManager _sceneItemManager;
        [SerializeField]
        private GameMenuUIController _gameMenuUIController;
        [SerializeField]
        private InventoryUIController _inventoryUIController;
        [SerializeField]
        private ConsumableHotbarUIController _consumableHotbarUIController;
        [SerializeField]
        private PickupItemTipUIController _pickupItemTipUIController;
        [SerializeField]
        private AudioManager _audioManager;

        private void Awake()
        {
            EventBus = _eventBusProvider.Instance;
            // ActorDomain
            Repository<Actor> actorRepository = new Repository<Actor>();
            ActorController = new ActorController(new CreateActorUseCase(EventBus, actorRepository),
                                                  new ChangeActorDirectionUseCase(EventBus, actorRepository),
                                                  new ChangeActorRunStateUseCase(EventBus, actorRepository),
                                                  new ResetActorLocomotionStateUseCase(EventBus, actorRepository),
                                                  new MakeActorPerformActionUseCase(EventBus, actorRepository),
                                                  new ChangeActorWeaponUseCase(EventBus, actorRepository),
                                                  new ChangeActorMagicUseCase(EventBus, actorRepository),
                                                  new RemoveActorUseCase(EventBus, actorRepository),
                                                  new CommitActorStatIdUseCase(actorRepository),
                                                  new GetActorActionUseCase(actorRepository),
                                                  new GetActorStatIdUseCase(actorRepository),
                                                  new GetActorConfigIdUseCase(actorRepository),
                                                  new GetActorWeaponConfigIdUseCase(actorRepository),
                                                  new GetActorMagicConfigIdUseCase(actorRepository));
            // InventoryDomain
            Inventory inventory = new Inventory();
            InventoryController = new InventoryController(new StoreItemUseCase(EventBus, inventory),
                                                          new EquipNewItemUseCase(EventBus, inventory),
                                                          new EquipItemFromStorageUseCase(EventBus, inventory),
                                                          new UnequipItemUseCase(EventBus, inventory),
                                                          new UseConsumableUseCase(EventBus, inventory),
                                                          new DiscardItemUseCase(EventBus, inventory),
                                                          new IsExistItemUseCase(inventory),
                                                          new GetItemConfigIdUseCase(inventory),
                                                          new CanStoreItemUseCase(inventory),
                                                          new CanEquipItemFromStorageUseCase(inventory));
            // StatDomain
            Repository<Stat> statRepository = new Repository<Stat>();
            StatController = new StatController(new CreateStatUseCase(EventBus, statRepository),
                                                new ModifyStatValueUseCase(EventBus, statRepository),
                                                new RemoveStatUseCase(EventBus, statRepository),
                                                new GetStatValueUseCase(statRepository));
            // ActorEvent
            ActorCreatedHandler actorCreatedHandler = new ActorCreatedHandler(this);
            EventBus.Register<ActorDirectionChanged>(e => _actorPresenter.OnActorDirectionChanged(e.Id, e.Direction.X, e.Direction.Y));
            EventBus.Register<ActorRunStateChanged>(e => _actorPresenter.OnActorRunStateChanged(e.Id, e.IsRun));
            EventBus.Register<ActorLocomotionStateReseted>(e => _actorPresenter.OnActorLocomotionStateReseted(e.Id));
            ActorActionPerformedHandler actorActionPerformedHandler = new ActorActionPerformedHandler(this);
            EventBus.Register<ActorWeaponChanged>(e => { WeaponConfig wc = _itemConfigOverView[e.WeaponConfigId] as WeaponConfig; _actorPresenter.OnActorWeaponChanged(e.Id, wc.RuntimeAnimatorController, wc.Prefab); });
            EventBus.Register<ActorMagicChanged>(e => _actorPresenter.OnActorMagicChanged(e.Id, e.MagicConfigId == string.Empty ? null : (_itemConfigOverView[e.MagicConfigId] as MagicConfig).Prefab));
            EventBus.Register<ActorRemoved>(e => { _actorPresenter.OnActorRemoved(e.Id); foreach (var statId in e.StatIdsByType.Values) StatController.RemoveStat(statId); });
            // InventoryEvent
            SlotModifiedHandler slotModifiedHandler = new SlotModifiedHandler(this);
            // StatEvent
            StatCreatedHandler statCreatedHandler = new StatCreatedHandler(this);
            StatValueModifiedHandler statValueModifiedHandler = new StatValueModifiedHandler(this);
            EventBus.Register<StatRemoved>(e => _statPresenter.OnStatRemoved(e.Id));
            // InputEvent
            EventBus.Register<DirectionInputTriggered>(e => ActorController.ChangeActorDirection(_actorPresenter.PlayerActorId, e.X, e.Y));
            EventBus.Register<RunInputTriggered>(e => ActorController.ChangeActorRunState(_actorPresenter.PlayerActorId, e.IsRun));
            ActionInputTriggeredHandler actionInputTriggeredHandler = new ActionInputTriggeredHandler(this);
            EventBus.Register<OpenGameMenuInputTriggered>(e => _gameMenuUIController.Open());
            EventBus.Register<CloseGameMenuInputTriggered>(e => _gameMenuUIController.Close());
            EventBus.Register<OpenInventoryInputTriggered>(e => e.Result = _inventoryUIController.Open());
            EventBus.Register<CloseInventoryInputTriggered>(e => e.Result = _inventoryUIController.Close());
            EventBus.Register<CancelInventoryInputTriggered>(e => _inventoryUIController.Cancel());
            EventBus.Register<SwitchSelectedConsumableInputTriggered>(e => _consumableHotbarUIController.SwitchSelected(e.IsRight));
            // UIEvent
            InventorySlotSelectedHandler inventorySlotSelectedHandler = new InventorySlotSelectedHandler(this);
            InventorySlotClickedHandler inventorySlotClickedHandler = new InventorySlotClickedHandler(this);
            EventBus.Register<EquipButtonClicked>(e => InventoryController.EquipItemFromStorage(e.Index));
            DiscardButtonClickedHandler discardButtonClickedHandler = new DiscardButtonClickedHandler(this);
            // ViewEvent
            ActorSpawnerExecutedHandler actorSpawnerExecutedHandler = new ActorSpawnerExecutedHandler(this);
            EventBus.Register<LocomotionStateEntered>(e => ActorController.ResetActorLocomotionState(_actorPresenter[e.ViewObject]));
            PlayerActorCreatedHandler playerActorCreatedHandler = new PlayerActorCreatedHandler(this);
            EventBus.Register<PlayerActorDeathStarted>(e => { _inventoryUIController.CanOpen = false; _playerInputReceiver.OnPlayerActorDeathStarted(); });
            ActorRevivedHandler actorRevivedHandler = new ActorRevivedHandler(this);
            EventBus.Register<ActorAIDirectionChanged>(e => ActorController.ChangeActorDirection(_actorPresenter[e.ViewObject], e.Direction.x, e.Direction.y));
            EventBus.Register<ActorAIRunStateChanged>(e => ActorController.ChangeActorRunState(_actorPresenter[e.ViewObject], e.IsRun));
            EventBus.Register<ActorAIActionPerformed>(e => ActorController.MakeActorPerformAction(_actorPresenter[e.ViewObject], e.Action));
            EventBus.Register<EnemyActorDied>(e => { ActorController.RemoveActor(_actorPresenter[e.ViewObject]); _sceneItemManager.Create(e.ViewObject.transform.position, e.DropConfigId, 1); });
            HitboxTriggeredHandler hitboxTriggeredHandler = new HitboxTriggeredHandler(this);
            PlayerConsumableUsedHandler playerConsumableUsedHandler = new PlayerConsumableUsedHandler(this);
            SelectedSceneItemChangedHandler selectedSceneItemChangedHandler = new SelectedSceneItemChangedHandler(this);
            EventBus.Register<AudioEventTriggered>(_audioManager.OnAudioEventTriggered);
        }
    }
}