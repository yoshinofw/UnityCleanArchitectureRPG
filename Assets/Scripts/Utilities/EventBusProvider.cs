using UnityEngine;

namespace UCARPG.Utilities
{
    public class EventBusProvider : MonoBehaviour
    {
        public EventBus Instance { get => _eventBus; }
        private EventBus _eventBus = new EventBus();
    }
}