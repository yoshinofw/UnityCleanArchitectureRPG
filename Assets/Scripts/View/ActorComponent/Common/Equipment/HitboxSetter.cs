using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class HitboxSetter : MonoBehaviour
    {
        [SerializeField]
        private Transform _origin;
        [SerializeField]
        private Vector3 _halfExtents = new Vector3(1, 1, 1);
        [Range(0, 1)]
        [SerializeField]
        private float _colorA = .25f;

        public Vector3[] GetFaceCenters()
        {
            Vector3[] result = new Vector3[6];
            result[0] = _origin.position + _origin.forward * _halfExtents.z;
            result[1] = _origin.position - _origin.forward * _halfExtents.z;
            result[2] = _origin.position + _origin.right * _halfExtents.x;
            result[3] = _origin.position - _origin.right * _halfExtents.x;
            result[4] = _origin.position + _origin.up * _halfExtents.y;
            result[5] = _origin.position - _origin.up * _halfExtents.y;
            return result;
        }

        private void OnDrawGizmos()
        {
            if (_origin == null)
            {
                return;
            }
            Gizmos.matrix = _origin.localToWorldMatrix;
            Gizmos.color = new Color(0, 1, 0, _colorA);
            Gizmos.DrawCube(Vector3.zero, _halfExtents * 2);
        }
    }
}