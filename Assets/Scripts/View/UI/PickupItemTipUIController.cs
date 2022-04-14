using UnityEngine;
using UnityEngine.UI;

namespace UCARPG.View.UI
{
    public class PickupItemTipUIController : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void Show(bool canPickup)
        {
            _text.text = canPickup ? "Pickup item" : "Inventory full";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
