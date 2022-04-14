using System.Collections.Generic;

namespace UCARPG.Domain.InventoryDomain.Entities
{
    public class Inventory
    {
        public List<object> Events { get; private set; }
        public SlotSet this[string name] { get => _slotSetsByName[name]; }
        private Dictionary<string, SlotSet> _slotSetsByName = new Dictionary<string, SlotSet>();

        public Inventory()
        {
            Events = new List<object>();
            AddSlotSet(new SlotSet("Weapon", 1, Events));
            AddSlotSet(new SlotSet("Magic", 1, Events));
            AddSlotSet(new SlotSet("Consumable", 3, Events));
            AddSlotSet(new SlotSet("Storage", 10, Events));
        }

        private void AddSlotSet(SlotSet slotSet)
        {
            _slotSetsByName.Add(slotSet.Name, slotSet);
        }
    }
}