using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class HitboxTriggered
    {
        public GameObject Attacker { get; private set; }
        public GameObject Target { get; private set; }
        public string Type { get; private set; }
        public Vector3 Direction { get; private set; }

        public HitboxTriggered(GameObject attacker, GameObject target, string type, Vector3 direction)
        {
            Attacker = attacker;
            Target = target;
            Type = type;
            Direction = direction;
        }
    }
}