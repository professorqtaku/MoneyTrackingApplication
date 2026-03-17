namespace Money_Tracking_Application.components
{
    internal class MoneyTracking
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void EditItem(Item oldItem, Item newItem)
        {
            int index = Items.IndexOf(oldItem);
            if (index != -1)
            {
                Items[index] = newItem;
            }
        }

        public void RemoveItem(Item item) {
            Items.Remove(item);
        }

        public void ShowItems()
        {
            foreach (var item in Items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
