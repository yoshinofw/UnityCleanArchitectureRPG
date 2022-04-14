using UnityEngine;
using UCARPG.Utilities;
using UCARPG.Domain.ActorDomain.InterfaceAdapters;

namespace UCARPG.View.ActorComponent
{
    public class ActorComponentEventBusInjector : MonoBehaviour, IActorComponentEventBusInjector
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private MotionPerformer _motionPerformer;
        private EventBus _eventBus;

        public void Inject(MonoBehaviour eventBusProvider)
        {
            _eventBus = (eventBusProvider as EventBusProvider).Instance;
            foreach (var eventBusUser in GetComponents<IEventBusUser>())
            {
                eventBusUser.EventBus = _eventBus;
            }
            _motionPerformer.RuntimeAnimatorControllerChanged += InjectEventBusIntoStateMachineBehaviours;
            Destroy(this);
        }

        private void InjectEventBusIntoStateMachineBehaviours()
        {
            foreach (var stateMachineBehaviour in _animator.GetBehaviours<StateMachineBehaviour>())
            {
                if (stateMachineBehaviour is IEventBusUser eventBusUser)
                {
                    eventBusUser.EventBus = _eventBus;
                }
            }
        }
    }
}