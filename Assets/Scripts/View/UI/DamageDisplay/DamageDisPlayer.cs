using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.UI
{
    public class DamageDisPlayer : MonoBehaviour
    {
        [SerializeField]
        private DamageText _damageTextPrefab;
        [SerializeField]
        private Transform _canvas;
        [SerializeField]
        private int _initialCount;
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private Vector3 _offset = new Vector3(0, 2, 0);
        private ObjectPool<DamageText> _damageTextPool;

        public void Display(GameObject targetActorViewObject, float value)
        {
            Vector3 position = _camera.WorldToScreenPoint(targetActorViewObject.transform.position + _offset);
            _damageTextPool.GetObject().Play(value.ToString(), position);
        }

        private void Awake()
        {
            _damageTextPool = new ObjectPool<DamageText>(_damageTextPrefab, _canvas, _initialCount);
        }
    }
}