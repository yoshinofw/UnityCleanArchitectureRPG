using System.Collections;
using UnityEngine;
using UCARPG.Utilities;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class PlayerDeathHandler : MonoBehaviour, IEventBusUser, IRevivableActor
    {
        public EventBus EventBus { get; set; }
        private Vector3 _revivePosition;
        [SerializeField]
        private GameObject _triggerCollider;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private MotionPerformer _motionPerformer;
        [SerializeField]
        private float _reviveWaitTime = 1;
        private IParticleSystemPlayer _particleSystemPlayer;

        public void SetRevivePosition(Vector3 position)
        {
            _revivePosition = position;
        }

        private void Awake()
        {
            _motionPerformer.ActionPerformed += action => { if (action == "Death") OnDeathStarted(); };
            _motionPerformer.RuntimeAnimatorControllerChanged += () => _animator.GetBehaviour<DiedNotifier>().Died += () => StartCoroutine(OnDied());
            _particleSystemPlayer = GetComponent<IParticleSystemPlayer>();
        }

        private void OnDeathStarted()
        {
            _triggerCollider.SetActive(false);
            _characterController.enabled = false;
            EventBus.Post(new PlayerActorDeathStarted());
        }

        private IEnumerator OnDied()
        {
            _animator.enabled = false;
            _motionPerformer.enabled = false;
            yield return new WaitForSeconds(_reviveWaitTime);
            transform.position = _revivePosition;
            _triggerCollider.SetActive(true);
            _characterController.enabled = true;
            _animator.enabled = true;
            _motionPerformer.enabled = true;
            _animator.Play("Locomotion");
            _particleSystemPlayer.Play("Revived");
            EventBus.Post(new PlayerActorRevived());
        }
    }
}