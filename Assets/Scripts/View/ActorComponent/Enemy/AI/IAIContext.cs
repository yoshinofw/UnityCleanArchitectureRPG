using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public interface IAIContext
    {
        Transform Transform { get; }
        Vector3[] PatrolPath { get; }
        int PatrolTargetIndex { get; set; }
        float PatrolTargetTolerance { get; }
        float DwellTime { get; }
        Transform ChaseTarget { get; set; }
        float ChaseDistance { get; }
        float AttackDistance { get; }
        float HalfAttackAngle { get; }
        float AttackCD { get; }
        IAIState State { set; }
        void ChangeDirection(Vector3 direction);
        void ChangeRunState(bool isRun);
        void PerformAction(string actionName);
    }
}