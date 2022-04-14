using UCARPG.ScriptableObjects;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;
using UCARPG.Domain.StatDomain.InterfaceAdapters;
using UCARPG.View.ActorComponent;
using UCARPG.View.UI;

namespace UCARPG.View.Core
{
    public class HitboxTriggeredHandler
    {
        private ItemConfigOverView _itemConfigOverView;
        private ActorController _actorController;
        private ActorPresenter _actorPresenter;
        private DamageDisPlayer _damageDisPlayer;
        private StatController _statController;

        public HitboxTriggeredHandler(IGameSystemProvider gameSystemProvider)
        {
            gameSystemProvider.EventBus.Register<HitboxTriggered>(HandleEvent);
            _itemConfigOverView = gameSystemProvider.ItemConfigOverView;
            _actorController = gameSystemProvider.ActorController;
            _actorPresenter = gameSystemProvider.ActorPresenter;
            _damageDisPlayer = gameSystemProvider.DamageDisPlayer;
            _statController = gameSystemProvider.StatController;
        }

        private void HandleEvent(HitboxTriggered e)
        {
            string targetActorId = _actorPresenter[e.Target];
            if (_actorController.GetActorAction(targetActorId) == "Dodge")
            {
                return;
            }
            string attackerActorId = _actorPresenter[e.Attacker];
            float healthDamage;
            float poiseDamage;
            switch (e.Type)
            {
                case "Weapon":
                    WeaponConfig weaponConfig = _itemConfigOverView[_actorController.GetActorWeaponConfigId(attackerActorId)] as WeaponConfig;
                    healthDamage = weaponConfig.HealthDamage;
                    poiseDamage = weaponConfig.PoiseDamage;
                    break;
                case "Magic":
                    MagicConfig magicConfig = _itemConfigOverView[_actorController.GetActorMagicConfigId(attackerActorId)] as MagicConfig;
                    healthDamage = magicConfig.HealthDamage;
                    poiseDamage = magicConfig.PoiseDamage;
                    break;
                default:
                    throw new System.Exception();
            }
            _damageDisPlayer.Display(e.Target, healthDamage);
            _statController.ModifyStatValue(_actorController.GetActorStatId(targetActorId, "Health"), -healthDamage);
            _actorPresenter.OnHitboxTriggered(targetActorId, e.Direction);
            _statController.ModifyStatValue(_actorController.GetActorStatId(targetActorId, "Poise"), -poiseDamage);
        }
    }
}