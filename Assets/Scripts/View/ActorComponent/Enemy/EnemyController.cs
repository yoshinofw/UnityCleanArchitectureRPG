using UnityEngine;
using UCARPG.Utilities;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class EnemyController : MonoBehaviour, IEventBusUser, IAIContext, IPatrolableActor
    {
        public bool Enabled { set => enabled = value; }
        public EventBus EventBus { get; set; }
        public Transform Transform { get => transform; }
        public Vector3[] PatrolPath { get; set; }
        public int PatrolTargetIndex { get; set; }
        public float PatrolTargetTolerance { get => _patrolTargetTolerance; }
        public float DwellTime { get => _dwellTime; }
        public Transform ChaseTarget { get; set; }
        public float ChaseDistance { get => _chaseDistance; }
        public float AttackDistance { get => _attackDistance; }
        public float HalfAttackAngle { get => _halfAttackAngle; }
        public float AttackCD { get => _attackCD; }
        public IAIState State { private get; set; }
        [SerializeField]
        private float _patrolTargetTolerance = 0.1f;
        [SerializeField]
        private float _dwellTime = 1;
        [SerializeField]
        private float _chaseDistance = 1.5f;
        [SerializeField]
        private float _attackDistance = 0.6f;
        [Range(0, 180)]
        [SerializeField]
        private float _halfAttackAngle = 20;
        [SerializeField]
        private float _attackCD = 1.5f;

        public void ChangeDirection(Vector3 direction)
        {
            EventBus.Post(new ActorAIDirectionChanged(gameObject, new Vector2(direction.x, direction.z).normalized));
        }

        public void ChangeRunState(bool isRun)
        {
            EventBus.Post(new ActorAIRunStateChanged(gameObject, isRun));
        }

        public void PerformAction(string action)
        {
            EventBus.Post(new ActorAIActionPerformed(gameObject, action));
        }

        private void Awake()
        {
            enabled = false;
        }

        private void Start()
        {
            State = new PatrolState(this);
        }

        private void Update()
        {
            State.Update();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = State?.GetType().Name == "FightState" ? Color.red : Color.green;
            Gizmos.DrawWireSphere(Vector3.zero, ChaseDistance);
            float leftRadian = (90 + HalfAttackAngle) * Mathf.Deg2Rad;
            float rightRadian = (90 - HalfAttackAngle) * Mathf.Deg2Rad;
            Vector3 leftDirection = new Vector3(Mathf.Cos(leftRadian) * AttackDistance, 0, Mathf.Sin(leftRadian) * AttackDistance);
            Vector3 rightDirection = new Vector3(Mathf.Cos(rightRadian) * AttackDistance, 0, Mathf.Sin(rightRadian) * AttackDistance);
            Gizmos.DrawRay(Vector3.zero, leftDirection);
            Gizmos.DrawRay(Vector3.zero, rightDirection);
            Gizmos.DrawRay(leftDirection, rightDirection - leftDirection);
        }
    }
}