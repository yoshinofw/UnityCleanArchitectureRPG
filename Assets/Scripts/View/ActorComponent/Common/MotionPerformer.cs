using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class MotionPerformer : MonoBehaviour, IMotionPerformer
    {
        public event Action RuntimeAnimatorControllerChanged;
        public event Action GetHitDirectionChanged;
        public event Action<string> ActionPerformed;
        public RuntimeAnimatorController RuntimeAnimatorController { set { _animator.runtimeAnimatorController = value; RuntimeAnimatorControllerChanged?.Invoke(); } }
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private CharacterController _characterController;
        private Vector3 _direction;
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private float _speed;
        private float _speedCurrentVelocity;
        [SerializeField]
        private float _speedSmoothTime = 0.15f;
        [SerializeField]
        private float _rotateSpeed = 0.1f;
        [SerializeField]
        private float _runSpeedMagnification = 2;
        private float _speedMagnification = 1;
        private bool _isLocomotion;
        private readonly int _getHitX = Animator.StringToHash("GetHitX");
        private readonly int _getHitY = Animator.StringToHash("GetHitY");
        private ReadOnlyDictionary<string, int> _animatorTriggerHashsByAction;

        public void ChangeDirection(Vector3 direction)
        {
            _direction = direction;
            _speed = _direction.magnitude == 0 ? 0 : 1;
        }

        public void ChangeRunState(bool isRun)
        {
            _speedMagnification = isRun ? _runSpeedMagnification : 1;
        }

        public void ResetLocomotionState()
        {
            _isLocomotion = true;
        }

        public void PerformAction(string action)
        {
            _isLocomotion = false;
            if (action == "Dodge")
            {
                transform.forward = _direction.magnitude == 0 ? transform.forward : _direction;
            }
            if (action == "Death")
            {
                foreach (var hash in _animatorTriggerHashsByAction.Values)
                {
                    _animator.ResetTrigger(hash);
                }
            }
            _animator.SetTrigger(_animatorTriggerHashsByAction[action]);
            ActionPerformed?.Invoke(action);
        }

        public void SetGetHitDirection(Vector3 direction)
        {
            direction = transform.InverseTransformDirection(direction);
            direction.y = 0;
            direction.Normalize();
            _animator.SetFloat(_getHitX, direction.x);
            _animator.SetFloat(_getHitY, direction.z);
            GetHitDirectionChanged?.Invoke();
        }

        private void Awake()
        {
            Dictionary<string, int> animatorTriggerHashsByAction = new Dictionary<string, int>();
            animatorTriggerHashsByAction.Add("Dodge", Animator.StringToHash("Dodge"));
            animatorTriggerHashsByAction.Add("Attack", Animator.StringToHash("Attack"));
            animatorTriggerHashsByAction.Add("Cast", Animator.StringToHash("Cast"));
            animatorTriggerHashsByAction.Add("Pickup", Animator.StringToHash("Pickup"));
            animatorTriggerHashsByAction.Add("Use", Animator.StringToHash("Use"));
            animatorTriggerHashsByAction.Add("Death", Animator.StringToHash("Death"));
            animatorTriggerHashsByAction.Add("GetHit", Animator.StringToHash("GetHit"));
            _animatorTriggerHashsByAction = new ReadOnlyDictionary<string, int>(animatorTriggerHashsByAction);
        }

        private void Update()
        {
            _animator.SetFloat(_speedHash, Mathf.SmoothDamp(_animator.GetFloat(_speedHash), _speed * _speedMagnification, ref _speedCurrentVelocity, _speedSmoothTime));
        }

        private void OnAnimatorMove()
        {
            if (!_characterController.enabled)
            {
                _animator.ApplyBuiltinRootMotion();
            }
            else if (_isLocomotion)
            {
                _characterController.Move(_animator.deltaPosition.magnitude * _direction);
                if (_direction.magnitude != 0)
                {
                    transform.forward = Vector3.Slerp(transform.forward, _direction, _rotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                _characterController.Move(_animator.deltaPosition);
                transform.Rotate(_animator.deltaRotation.eulerAngles);
            }
        }
    }
}