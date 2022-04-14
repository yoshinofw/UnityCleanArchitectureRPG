using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.ActorComponent
{
    public class ActorSpawner : MonoBehaviour
    {
        [SerializeField]
        EventBusProvider _eventBusProvider;
        [SerializeField]
        private string _configId;
        [SerializeField]
        private bool _isSpawnOnStart = true;
        [Header("Spawn point gizmos")]
        [SerializeField]
        private float _radius = 0.1f;
        [SerializeField]
        private Color _color = Color.white;

        public void Execute(Vector3[] patrolPath = null)
        {
            _eventBusProvider.Instance.Post(new ActorSpawnerExecuted(_configId, transform.position, transform.rotation, patrolPath));
        }

        private void Start()
        {
            if (_isSpawnOnStart)
            {
                Execute();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}