using UnityEngine;

namespace UCARPG.Domain.ActorDomain.InterfaceAdapters
{
    public interface IMotionPerformer
    {
        RuntimeAnimatorController RuntimeAnimatorController { set; }
        void ChangeDirection(Vector3 direction);
        void ChangeRunState(bool isRun);
        void ResetLocomotionState();
        void PerformAction(string action);
        void SetGetHitDirection(Vector3 direction);
    }
}