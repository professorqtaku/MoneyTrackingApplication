namespace Money_Tracking_Application.components
{
    class Item(string title, float amount, DateTime? date, ItemType? type)
    {
        public string Title { get; set; } = title;
        public float Amount { get; set; } = amount;
        public DateTime Date { get; set; } = date ?? DateTime.Now;
        public ItemType Type { get; set; } = type ?? ItemType.Expense;

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
