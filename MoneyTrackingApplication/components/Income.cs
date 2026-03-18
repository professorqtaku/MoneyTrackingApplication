using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    internal class Income : Item
    {
        public Income(string title, float amount, DateTime? month) : base(title, amount, month)
        {
        }
    }
}
