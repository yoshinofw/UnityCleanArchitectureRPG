using UnityEngine;
using UnityEngine.UI;

namespace UCARPG.View.UI
{
    public class ConsumableHotbarUIController : MonoBehaviour
    {
        public int SelectedIndex { get; private set; }
        [SerializeField]
        private Image[] _slots;
        [SerializeField]
        private Color _normalColor;
        [SerializeField]
        private Color _selectedColor;

        public void SwitchSelected(bool isRight)
        {
            _slots[SelectedIndex].color = _normalColor;
            SelectedIndex = (int)Mathf.Repeat(SelectedIndex + (isRight ? 1 : -1), _slots.Length);
            _slots[SelectedIndex].color = _selectedColor;
        }

        private void Awake()
        {
            foreach (var slot in _slots)
            {
                slot.color = _normalColor;
            }
            if (_slots.Length > 0)
            {
                _slots[SelectedIndex].color = _selectedColor;
            }
        }
    }
}