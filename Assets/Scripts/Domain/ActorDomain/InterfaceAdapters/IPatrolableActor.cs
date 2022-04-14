using UnityEngine;

namespace UCARPG.Domain.ActorDomain.InterfaceAdapters
{
    public interface IPatrolableActor
    {
        bool Enabled { set; }
        Vector3[] PatrolPath { set; }
        Transform ChaseTarget { set; }
    }
}