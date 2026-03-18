namespace Money_Tracking_Application.components
{
    public enum ItemType
    {
        INCOME,
        EXPENSE
    }

    class Item
    {
        public string Title { get; set; } = "";
        public float Amount { get; set; } = 0;
        public DateTime Date { get; set; }
        public ItemType Type { get; set; }

        public Item(string title, float amount, DateTime? date, ItemType? type)
        {
            Title = title;
            Amount = amount;
            Date = date ?? DateTime.Now;
            Type = type ?? ItemType.EXPENSE;
        }

        public Item SetItem(string title, float amount, DateTime date)
        {
            Title = title;
            Amount = amount;
            Date = date;
            return this;
        }

        public override string ToString() { 
            return $"Item {Title} {Amount}kr, {Date}";
        }

    }
}
