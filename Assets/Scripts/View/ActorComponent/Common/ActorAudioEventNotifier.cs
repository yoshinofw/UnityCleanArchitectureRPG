using System.Collections.Generic;
using UnityEngine;
using UCARPG.Utilities;
using UCARPG.View.Audio;

namespace UCARPG.View.ActorComponent
{
    public class ActorAudioEventNotifier : MonoBehaviour, IEventBusUser
    {
        public EventBus EventBus { get; set; }
        [SerializeField]
        Animator _animator;
        [SerializeField]
        MotionPerformer _motionPerformer;
        [SerializeField]
        private AudioClip[] _walkAudioClips;
        [SerializeField]
        private AudioClip[] _runAudioClips;
        [SerializeField]
        private AudioClip _dodgeAudioClip;
        [SerializeField]
        private AudioClip[] _fistAudioClips;
        [SerializeField]
        private AudioClip[] _DaggerAudioClips;
        [SerializeField]
        private AudioClip[] _SpearAudioClips;
        [SerializeField]
        private AudioClip[] _swordAudioClips;
        [SerializeField]
        private AudioClip _castAudioClip;
        [SerializeField]
        private AudioClip _useupAudioClip;
        [SerializeField]
        private AudioClip _pickupAudioClip;
        [SerializeField]
        private AudioClip[] _getHitAudioClips;
        private Dictionary<string, AudioClip[]> _attackAudioClipArraysByWeapon = new Dictionary<string, AudioClip[]>();

        private void Awake()
        {
            _motionPerformer.ActionPerformed += OnActionPerformed;
            _motionPerformer.GetHitDirectionChanged += () => TriggerAudioEvent(_getHitAudioClips[Random.Range(0, _getHitAudioClips.Length)]);
            _attackAudioClipArraysByWeapon.Add("Prototype", _fistAudioClips);
            _attackAudioClipArraysByWeapon.Add("Dagger", _DaggerAudioClips);
            _attackAudioClipArraysByWeapon.Add("Spear", _SpearAudioClips);
            _attackAudioClipArraysByWeapon.Add("Sword", _swordAudioClips);
            _attackAudioClipArraysByWeapon.Add("Axe", _swordAudioClips);
            _attackAudioClipArraysByWeapon.Add("GreatSword", _swordAudioClips);
        }

        private void Walk(AnimationEvent e)
        {
            if (e.animatorClipInfo.weight > 0.5)
            {
                TriggerAudioEvent(_walkAudioClips[Random.Range(0, _walkAudioClips.Length)]);
            }
        }

        private void Run(AnimationEvent e)
        {
            if (e.animatorClipInfo.weight > 0.5)
            {
                TriggerAudioEvent(_runAudioClips[Random.Range(0, _runAudioClips.Length)]);
            }
        }

        private void OnActionPerformed(string action)
        {
            switch (action)
            {
                case "Dodge":
                    TriggerAudioEvent(_dodgeAudioClip);
                    break;
                case "Attack":
                    AudioClip[] audioClips = _attackAudioClipArraysByWeapon[_animator.runtimeAnimatorController.name];
                    TriggerAudioEvent(audioClips[Random.Range(0, audioClips.Length)]);
                    break;
                case "Cast":
                    TriggerAudioEvent(_castAudioClip, 0.4f, 0.5f, 2f, 2.5f);
                    break;
                case "Use":
                    TriggerAudioEvent(_useupAudioClip, 0.4f, 0.5f, 1.2f, 1.5f);
                    break;
                case "Pickup":
                    TriggerAudioEvent(_pickupAudioClip);
                    break;
            }
        }

        private void TriggerAudioEvent(AudioClip audioClip, float minvolume = 0.4f, float maxvolume = 0.5f, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            float volume = Random.Range(minvolume, maxvolume);
            float pitch = Random.Range(minPitch, maxPitch);
            EventBus.Post(new AudioEventTriggered(audioClip, volume, pitch, 1, transform.position));
        }
    }
}