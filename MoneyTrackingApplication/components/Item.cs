namespace Money_Tracking_Application.components
{
    class Item
    {
        public string Title { get; set; } = "";
        public float Amount { get; set; } = 0;
        public DateTime Month { get; set; }

        public Item(string title, float amount, DateTime? month)
        {
            Title = title;
            Amount = amount;
            Month = month ?? DateTime.Now;
        }

        public Item SetItem(string title, float amount, DateTime month)
        {
            Title = title;
            Amount = amount;
            Month = month;
            return this;
        }

        public override string ToString() { 
            return $"Item {Title} {Amount}kr, {Month}";
        }

    }
}
