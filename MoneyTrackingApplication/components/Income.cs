using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    internal class Income : Item
    {
        public Income(string title, float amount, Month? month) : base(title, amount, month)
        {
        }
    }
}
