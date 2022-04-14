using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class EnemyActorDied
    {
        public GameObject ViewObject { get; private set; }
        public string DropConfigId { get; private set; }

        public EnemyActorDied(GameObject viewObject, string dropConfigId)
        {
            ViewObject = viewObject;
            DropConfigId = dropConfigId;
        }
    }
}