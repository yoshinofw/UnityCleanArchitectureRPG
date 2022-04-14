namespace UCARPG.View.UI
{
    public class InventorySlotClicked
    {
        public string SetName { get; private set; }
        public int Index { get; private set; }

        public InventorySlotClicked(string setName, int index)
        {
            SetName = setName;
            Index = index;
        }
    }
}