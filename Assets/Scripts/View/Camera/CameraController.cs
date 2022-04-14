using UnityEngine;

namespace UCARPG.View.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform FollowTarget { get; set; }
        [SerializeField]
        private Transform _camera;
        [SerializeField]
        private Vector3 _rotation = new Vector3(45, -45, 0);
        [SerializeField]
        private Vector3 _positionOffset = new Vector3(2.5f, 4, -2.5f);

        private void Awake()
        {
            _camera.rotation = Quaternion.Euler(_rotation);
        }

        private void LateUpdate()
        {
            _camera.position = FollowTarget.position + _positionOffset;
        }
    }
}