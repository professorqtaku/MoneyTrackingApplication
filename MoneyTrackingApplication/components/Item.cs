using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    class Item
    {
        public string Title { get; set; } = "";
        public float Amount { get; set; } = 0;
        public Month Month { get; set; }

        public Item(string title, float amount, Month? month)
        {
            Title = title;
            Amount = amount;
            Month = month ?? (Month)DateTime.Now.Month;
        }

        public Item SetItem(string title, float amount, Month month)
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
