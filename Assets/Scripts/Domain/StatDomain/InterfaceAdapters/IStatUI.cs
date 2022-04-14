using UnityEngine;

namespace UCARPG.Domain.StatDomain.InterfaceAdapters
{
    public interface IStatUI
    {
        float MaxValue { set; }
        float Value { set; }
        Transform FollowTarget { set; }
        public Camera Camera { set; }
    }
}