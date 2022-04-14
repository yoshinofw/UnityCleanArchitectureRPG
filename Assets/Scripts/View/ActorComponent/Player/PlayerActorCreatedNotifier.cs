using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.ActorComponent
{
    public class PlayerActorCreatedNotifier : MonoBehaviour, IEventBusUser
    {
        public EventBus EventBus { get; set; }

        void Start()
        {
            EventBus.Post(new PlayerActorCreated(gameObject));
            Destroy(this);
        }
    }
}