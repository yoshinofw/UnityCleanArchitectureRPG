using UCARPG.Utilities;
using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class PlayerUseConsumableNotifier : MonoBehaviour, IEventBusUser
    {
        public EventBus EventBus { get; set; }

        private void Use()
        {
            EventBus.Post(new PlayerConsumableUsed());
        }
    }
}