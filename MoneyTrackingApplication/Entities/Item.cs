using MoneyTrackingApplication.Enums;

namespace MoneyTrackingApplication.Entities
{
    class Item(string title, double amount, DateTime? date, ItemType? type)
    {
        public string Title { get; set; } = title;
        public double Amount { get; set; } = amount;
        public DateTime Date { get; set; } = date ?? DateTime.Now;
        public ItemType Type { get; set; } = type ?? ItemType.Expense;

        public Item SetItem(string title, double amount, DateTime date, ItemType type)
        {
            Title = title;
            Amount = amount;
            Date = date;
            Type = type;
            return this;
        }

        public override string ToString() { 
            return $"Item {Title} {Amount}kr, {Date}";
        }

    }
}
