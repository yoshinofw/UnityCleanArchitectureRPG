namespace UCARPG.Domain.InventoryDomain.Entities
{
    public class SlotCleared
    {
        public string SetName { get; private set; }
        public int Index { get; private set; }

        public SlotCleared(string setName, int index)
        {
            SetName = setName;
            Index = index;
        }
    }
}