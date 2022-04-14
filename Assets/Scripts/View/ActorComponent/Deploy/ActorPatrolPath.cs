using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class ActorPatrolPath : MonoBehaviour
    {
        [SerializeField]
        private ActorSpawner _actorSpawner;
        [SerializeField]
        private Transform[] _wayPoints;
        [Header("Way points gizmos")]
        [SerializeField]
        private float _radius = 0.1f;
        [SerializeField]
        private Color _color = Color.black;

        private void Start()
        {
            int pointCount = _wayPoints.Length;
            Vector3[] patrolPath = new Vector3[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                patrolPath[i] = _wayPoints[i].position;
            }
            _actorSpawner.Execute(patrolPath);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            int pointCount = _wayPoints.Length;
            if (_actorSpawner != null && pointCount > 0)
            {
                Gizmos.DrawLine(_actorSpawner.transform.position, _wayPoints[0].position);
            }
            for (int i = 0; i < pointCount; i++)
            {
                Vector3 position = _wayPoints[i].position;
                Gizmos.DrawSphere(position, _radius);
                Gizmos.DrawLine(position, _wayPoints[(int)Mathf.Repeat(i + 1, pointCount)].position);
            }
        }
    }
}