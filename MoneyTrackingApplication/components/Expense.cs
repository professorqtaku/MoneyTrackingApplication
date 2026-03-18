using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    internal class Expense : Item
    {
        public Expense(string title, float amount, DateTime? month) : base(title, amount, month)
        {
        }
    }
}
