using UnityEngine;
using UCARPG.Utilities;
using UCARPG.View.Audio;

namespace UCARPG.View.Mechanism
{
    public class Gate : MonoBehaviour
    {
        [SerializeField]
        private EventBusProvider _eventBusProvider;
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        private Animation _animation;
        [SerializeField]
        private AudioClip _audioClip;
        private AnimationState _animationState;
        private bool _isOpen;
        private bool _isClose;

        private void Awake()
        {
            _animationState = _animation["GateOpen"];
            _animationState.speed = -1;
        }

        private void LateUpdate()
        {
            if (_animation.isPlaying)
            {
                _collider.gameObject.layer = LayerMask.NameToLayer("Default");
                return;
            }
            if (_animationState.speed == 1)
            {
                _collider.gameObject.layer = LayerMask.NameToLayer("Special");
            }
            if (_isOpen)
            {
                _isOpen = false;
                _animationState.speed = 1;
                _animation.Play();
                _eventBusProvider.Instance.Post(new AudioEventTriggered(_audioClip, 0.5f, 1, 1, transform.position));
            }
            else if (_isClose)
            {
                _isClose = false;
                _animationState.speed = -1;
                _animationState.time = _animationState.length;
                _animation.Play();
                _eventBusProvider.Instance.Post(new AudioEventTriggered(_audioClip, 0.5f, 1, 1, transform.position));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _isOpen = !_animation.isPlaying || (_animation.isPlaying && _animationState.speed == -1);
                _isClose = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _isOpen = false;
                _isClose = !_animation.isPlaying || (_animation.isPlaying && _animationState.speed == 1);
            }
        }
    }
}