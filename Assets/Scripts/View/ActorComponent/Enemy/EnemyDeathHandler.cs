using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.ActorComponent
{
    public class EnemyDeathHandler : MonoBehaviour, IEventBusUser
    {
        public EventBus EventBus { get; set; }
        [SerializeField]
        private GameObject _triggerCollider;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private EnemyController _enemyController;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private MotionPerformer _motionPerformer;
        [SerializeField]
        private string[] _dropConfigIds;

        private void Awake()
        {
            _motionPerformer.ActionPerformed += action => { if (action == "Death") OnDeathStarted(); };
            _motionPerformer.RuntimeAnimatorControllerChanged += OnDied;
        }

        private void OnDeathStarted()
        {
            _triggerCollider.SetActive(false);
            _characterController.enabled = false;
            _enemyController.enabled = false;
        }

        private void OnDied()
        {
            _animator.GetBehaviour<DiedNotifier>().Died += () => EventBus.Post(new EnemyActorDied(gameObject, _dropConfigIds[Random.Range(0, _dropConfigIds.Length)]));
        }
    }
}