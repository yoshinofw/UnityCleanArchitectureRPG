using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class ActorAIDirectionChanged
    {
        public GameObject ViewObject { get; private set; }
        public Vector2 Direction { get; private set; }

        public ActorAIDirectionChanged(GameObject viewObject, Vector2 direction)
        {
            ViewObject = viewObject;
            Direction = direction;
        }
    }
}