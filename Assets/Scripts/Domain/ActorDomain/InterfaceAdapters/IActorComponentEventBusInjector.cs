using UnityEngine;

namespace UCARPG.Domain.ActorDomain.InterfaceAdapters
{
    public interface IActorComponentEventBusInjector
    {
        void Inject(MonoBehaviour eventBusProvider);
    }
}