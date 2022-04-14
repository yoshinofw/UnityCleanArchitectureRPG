namespace UCARPG.Domain.InventoryDomain.Entities
{
    public class ItemModified
    {
        public string SetName { get; private set; }
        public int Index { get; private set; }
        public string ItemConfigId { get; private set; }
        public int ItemCount { get; private set; }

        public ItemModified(string setName, int index, Item item)
        {
            SetName = setName;
            Index = index;
            ItemConfigId = item.ConfigId;
            ItemCount = item.Count;
        }
    }
}