using System;
using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class DiedNotifier : StateMachineBehaviour
    {
        public event Action Died;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            Died?.Invoke();
        }
    }
}