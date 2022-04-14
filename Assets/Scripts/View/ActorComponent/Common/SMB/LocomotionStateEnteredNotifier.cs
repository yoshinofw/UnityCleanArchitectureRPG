using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.ActorComponent
{
    public class LocomotionStateEnteredNotifier : StateMachineBehaviour, IEventBusUser
    {
        public EventBus EventBus { get; set; }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            EventBus.Post(new LocomotionStateEntered(animator.gameObject));
        }
    }
}