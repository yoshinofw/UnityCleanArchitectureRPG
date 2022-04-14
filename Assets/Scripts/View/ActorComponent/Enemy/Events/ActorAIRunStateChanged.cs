using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class ActorAIRunStateChanged
    {
        public GameObject ViewObject { get; private set; }
        public bool IsRun { get; private set; }

        public ActorAIRunStateChanged(GameObject viewObject, bool isRun)
        {
            ViewObject = viewObject;
            IsRun = isRun;
        }
    }
}