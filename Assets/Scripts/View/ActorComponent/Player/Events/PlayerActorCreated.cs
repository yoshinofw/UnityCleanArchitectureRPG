using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class PlayerActorCreated
    {
        public GameObject ViewObject { get; private set; }

        public PlayerActorCreated(GameObject viewObject)
        {
            ViewObject = viewObject;
        }
    }
}