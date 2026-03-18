namespace Money_Tracking_Application.components
{
    internal class ItemManager
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

        public void ViewItems()
        {
            foreach (var item in Items)
            {
                Console.WriteLine(item);
            }
        }

        public void ShowItemsByType<T>()
        {
            var list = Items.Where(item => item is T).ToList();
            list.ForEach(item => Console.WriteLine(item));
        }
    }
}
