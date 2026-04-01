using MoneyTrackingApplication.Enums;

namespace MoneyTrackingApplication.Entities
{
    public class Item
    {
        public string Title { get; set; } = string.Empty;
        public double Amount { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
        public ItemType Type { get; set; } = ItemType.Expense;

        // ✅ REQUIRED for System.Text.Json
        public Item() { }

        public Item(string title, double amount, DateTime date, ItemType type)
        {
            Title = title;
            Amount = amount;
            Date = date;
            Type = type;
        }

        public Item SetItem(string title, double amount, DateTime date, ItemType type)
        {
            Title = title;
            Amount = amount;
            Date = date;
            Type = type;
            return this;
        }

        public override string ToString()
        {
            return $"Item {Title} {Amount}kr, {Date}";
        }
    }
}