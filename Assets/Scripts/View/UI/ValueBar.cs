using UnityEngine;
using UnityEngine.UI;
using UCARPG.Domain.StatDomain.InterfaceAdapters;

namespace UCARPG.View.UI
{
    public class ValueBar : MonoBehaviour, IStatUI
    {
        public float MaxValue { set => _slider.maxValue = value; }
        public float Value { set => _slider.value = value; }
        public Transform FollowTarget { get; set; }
        public Camera Camera { get; set; }
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private bool _isFollow = false;
        [SerializeField]
        private Vector3 _offset;

        private void LateUpdate()
        {
            if (_isFollow)
            {
                transform.position = Camera.WorldToScreenPoint(FollowTarget.position + _offset);
            }
        }
    }
}