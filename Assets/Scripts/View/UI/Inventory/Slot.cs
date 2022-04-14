using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UCARPG.View.UI
{
    public class Slot : MonoBehaviour, ISelectHandler
    {
        public event Action Selected;
        public event Action<Sprite> IconChanged;
        public event Action<string> CountChanged;
        public Button Button { get => _button; }
        public Sprite Icon { get => _image.sprite; set { _image.sprite = value; IconChanged?.Invoke(value); } }
        public string Count { get => _text.text; set { _text.text = value; CountChanged?.Invoke(value); } }
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Sprite _defaultIcon;

        public void OnSelect(BaseEventData eventData)
        {
            Selected?.Invoke();
        }

        public void Clear()
        {
            Icon = _defaultIcon;
            Count = string.Empty;
        }
    }
}