using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UCARPG.Utilities;
using UCARPG.View.Audio;

namespace UCARPG.View.UI
{
    public class ButtonAudioEventNotifier : MonoBehaviour, ISelectHandler
    {
        public EventBus EventBus { get; set; }
        public AudioClip SelectAudioClip { get; set; }
        public AudioClip ClickAudioClip { get; set; }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => EventBus.Post(new AudioEventTriggered(ClickAudioClip, 1, 1, 0, Vector3.zero)));
        }

        public void OnSelect(BaseEventData eventData)
        {
            EventBus.Post(new AudioEventTriggered(SelectAudioClip, 1, 1, 0, Vector3.zero));
        }
    }
}