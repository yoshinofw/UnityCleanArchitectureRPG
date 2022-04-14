using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class ActorAIActionPerformed
    {
        public GameObject ViewObject { get; private set; }
        public string Action { get; private set; }

        public ActorAIActionPerformed(GameObject viewObject, string action)
        {
            ViewObject = viewObject;
            Action = action;
        }
    }
}