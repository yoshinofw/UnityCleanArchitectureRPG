using System;
using System.Collections.Generic;

namespace UCARPG.Domain.InventoryDomain.Entities
{
    public class SlotSet
    {
        public string Name { get; private set; }
        public int Length { get; private set; }
        public int Count { get => _slots.Count; }
        public bool IsAllSlotUsed { get => _slots.Count == Length; }
        public Item this[int index] { get => _slots[index]; }
        private List<object> _events;
        private List<Item> _slots = new List<Item>();

        public SlotSet(string name, int length, List<object> events)
        {
            Name = name;
            Length = length;
            _events = events;
        }

        public bool CanAccommodateNewItem(string configId, int count)
        {
            if (!IsAllSlotUsed)
            {
                return true;
            }
            int residualSpaceSum = 0;
            foreach (var slot in _slots)
            {
                if (slot.ConfigId == configId)
                {
                    residualSpaceSum += slot.ResidualSpace;
                }
            }
            return residualSpaceSum >= count;
        }

        public bool AddItemToNullSlot(Item item)
        {
            if (IsAllSlotUsed || item.Count == 0)
            {
                return false;
            }
            _slots.Add(item);
            _events.Add(new ItemModified(Name, _slots.Count - 1, item));
            return true;
        }

        public void StackItem(ref Item item)
        {
            if (item.Count == 0)
            {
                return;
            }
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].ConfigId == item.ConfigId && !_slots[i].IsFull)
                {
                    _slots[i].Stack(ref item);
                    _events.Add(new ItemModified(Name, i, _slots[i]));
                    if (item.Count == 0)
                    {
                        return;
                    }
                }
            }
        }

        public bool DecreaseItem(int index, int value)
        {
            if (value <= 0)
            {
                throw new Exception("Value should greater than 0");
            }
            Item item = _slots[index];
            if (item.ModifyCount(-value) < 0)
            {
                return false;
            }
            if (item.Count > 0)
            {
                _events.Add(new ItemModified(Name, index, item));
            }
            else
            {
                RemoveItem(index);
            }
            return true;
        }

        public void RemoveItem(int index)
        {
            _slots.RemoveAt(index);
            for (int i = index; i < _slots.Count; i++)
            {
                _events.Add(new ItemModified(Name, i, _slots[i]));
            }
            _events.Add(new SlotCleared(Name, _slots.Count));
        }
    }
}