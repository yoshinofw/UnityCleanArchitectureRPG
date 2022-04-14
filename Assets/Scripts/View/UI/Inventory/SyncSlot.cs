using UnityEngine;
using UnityEngine.UI;

namespace UCARPG.View.UI
{
    public class SyncSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Slot Target;

        private void Awake()
        {
            Target.IconChanged += icon => _image.sprite = icon;
            Target.CountChanged += count => _text.text = count;
        }
    }
}