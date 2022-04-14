using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UCARPG.Utilities;

namespace UCARPG.View.UI
{
    public class InventoryUIController : MonoBehaviour, IMenu
    {
        public event Action Opened;
        public event Action Closed;
        public bool CanOpen { get; set; }
        [SerializeField]
        private EventBusProvider _eventBusProvider;
        [SerializeField]
        private Selectable _firstSelected;
        [SerializeField]
        private GameObject _storageItemOptionMenu;
        [SerializeField]
        private Button _equipButton;
        [SerializeField]
        private Button _discardButton;
        [SerializeField]
        private GameObject _itemDescriptionBox;
        [SerializeField]
        private Text _itemDescriptionText;
        [SerializeField]
        private SlotSet[] _slotSets;
        private Dictionary<string, Slot[]> _slotSetsByName;
        private int _selectedStorageItemIndex;
        private string _lastShowItemDescriptionSlotName;
        private int _lastShowItemDescriptionSlotIndex;

        public bool Open()
        {
            if (CanOpen && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                _firstSelected.Select();
                Opened?.Invoke();
                return true;
            }
            return false;
        }

        public bool Close()
        {
            if (!_storageItemOptionMenu.activeSelf && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                Closed?.Invoke();
                return true;
            }
            return false;
        }

        public void Cancel()
        {
            if (_storageItemOptionMenu.activeSelf)
            {
                _storageItemOptionMenu.SetActive(false);
                _slotSetsByName["Storage"][_selectedStorageItemIndex].Button.Select();
            }
        }

        public void OpenStorageItemOptionMenu(int index, bool equipButtonInteractable)
        {
            _storageItemOptionMenu.transform.position = _slotSetsByName["Storage"][index].transform.position;
            _equipButton.interactable = equipButtonInteractable;
            if (equipButtonInteractable)
            {
                _equipButton.Select();
            }
            else
            {
                _discardButton.Select();
            }
            _selectedStorageItemIndex = index;
            _storageItemOptionMenu.SetActive(true);
            _itemDescriptionBox.SetActive(false);
        }

        public void ModifySlot(string setName, int index, Sprite icon, string count)
        {
            _slotSetsByName[setName][index].Icon = icon;
            _slotSetsByName[setName][index].Count = count;
            if (setName == _lastShowItemDescriptionSlotName && index == _lastShowItemDescriptionSlotIndex)
            {
                _eventBusProvider.Instance.Post(new InventorySlotSelected(setName, index));
            }
        }

        public void ClearSlot(string setName, int index)
        {
            _slotSetsByName[setName][index].Clear();
            if (setName == _lastShowItemDescriptionSlotName && index == _lastShowItemDescriptionSlotIndex)
            {
                _itemDescriptionBox.SetActive(false);
            }
        }

        public void ShowItemDescription(string setName, int index, string content)
        {
            _itemDescriptionBox.transform.position = _slotSetsByName[setName][index].transform.position;
            _itemDescriptionText.text = content;
            _itemDescriptionBox.SetActive(true);
            _lastShowItemDescriptionSlotName = setName;
            _lastShowItemDescriptionSlotIndex = index;
        }

        public void HideItemDescription()
        {
            _itemDescriptionBox.SetActive(false);
        }

        private void Awake()
        {
            CanOpen = true;
            _slotSetsByName = new Dictionary<string, Slot[]>();
            foreach (var set in _slotSets)
            {
                _slotSetsByName.Add(set.Name, set.Slots);
                for (int i = 0; i < set.Slots.Length; i++)
                {
                    int index = i;
                    set.Slots[i].Button.onClick.AddListener(() => _eventBusProvider.Instance.Post(new InventorySlotClicked(set.Name, index)));
                    set.Slots[i].Selected += () => _eventBusProvider.Instance.Post(new InventorySlotSelected(set.Name, index));
                }
            }
            _equipButton.onClick.AddListener(() => { _eventBusProvider.Instance.Post(new EquipButtonClicked(_selectedStorageItemIndex)); Cancel(); });
            _discardButton.onClick.AddListener(() => { _eventBusProvider.Instance.Post(new DiscardButtonClicked(_selectedStorageItemIndex)); Cancel(); });
            gameObject.SetActive(false);
            _storageItemOptionMenu.SetActive(false);
            _itemDescriptionBox.SetActive(false);
        }
    }

    [Serializable]
    public class SlotSet
    {
        public string Name;
        public Slot[] Slots;
    }
}