using System;

namespace UCARPG.Domain.InventoryDomain.Entities
{
    public class Item
    {
        public string ConfigId { get; private set; }
        public string Type { get; private set; }
        public int Capacity { get; private set; }
        public int Count { get; private set; }
        public int ResidualSpace { get => Capacity - Count; }
        public bool IsFull { get => Count == Capacity; }

        public Item(string configId, string type, int capacity, int count)
        {
            if (count == 0 || count > capacity)
            {
                throw new Exception("Count should be [1, capacity]");
            }
            ConfigId = configId;
            Type = type;
            Capacity = capacity;
            Count = count;
        }

        public int ModifyCount(int modifier)
        {
            int newCount = Count + modifier;
            if (newCount > Capacity)
            {
                Count = Capacity;
                return newCount - Capacity;
            }
            else if (newCount < 0)
            {
                return -1;
            }
            Count = newCount;
            return 0;
        }

        public void Stack(ref Item item)
        {
            item.Count = ModifyCount(item.Count);
        }
    }
}