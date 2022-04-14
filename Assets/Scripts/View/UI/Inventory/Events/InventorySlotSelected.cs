namespace UCARPG.View.UI
{
    public class InventorySlotSelected
    {
        public string SetName { get; private set; }
        public int Index { get; private set; }

        public InventorySlotSelected(string setName, int index)
        {
            SetName = setName;
            Index = index;
        }
    }
}