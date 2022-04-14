using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class LocomotionStateEntered
    {
        public GameObject ViewObject { get; private set; }

        public LocomotionStateEntered(GameObject viewObject)
        {
            ViewObject = viewObject;
        }
    }
}