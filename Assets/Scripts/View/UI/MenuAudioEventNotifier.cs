using UnityEngine;
using UCARPG.Utilities;
using UCARPG.View.Audio;

namespace UCARPG.View.UI
{
    public class MenuAudioEventNotifier : MonoBehaviour
    {
        [SerializeField]
        private EventBusProvider _eventBusProvider;
        [SerializeField]
        private GameObject _menuGameObject;
        [SerializeField]
        private GameObject[] _buttons;
        [SerializeField]
        private AudioClip _openAudioClip;
        [SerializeField]
        private AudioClip _closeAudioClip;
        [SerializeField]
        private AudioClip _selectAudioClip;
        [SerializeField]
        private AudioClip _clickAudioClip;

        private void Awake()
        {
            IMenu menu = _menuGameObject.GetComponent<IMenu>();
            menu.Opened += () => _eventBusProvider.Instance.Post(new AudioEventTriggered(_openAudioClip, 1, 1, 0, Vector3.zero));
            menu.Closed += () => _eventBusProvider.Instance.Post(new AudioEventTriggered(_closeAudioClip, 1, 1, 0, Vector3.zero));
            foreach (var button in _buttons)
            {
                ButtonAudioEventNotifier buttonAudioEventNotifier = button.AddComponent<ButtonAudioEventNotifier>();
                buttonAudioEventNotifier.EventBus = _eventBusProvider.Instance;
                buttonAudioEventNotifier.SelectAudioClip = _selectAudioClip;
                buttonAudioEventNotifier.ClickAudioClip = _clickAudioClip;
            }
        }
    }
}