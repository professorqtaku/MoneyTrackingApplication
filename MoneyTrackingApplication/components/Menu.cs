namespace Money_Tracking_Application.components
{
    class Menu
    {
        public void ShowMenu()
        {
            Console.WriteLine("({0}) Show items (All/Expense(s)/Income(s))", 1);
            Console.WriteLine("({0}) Add New Expense/Income", 2);
            Console.WriteLine("({0}) Edit Item (edit, remove)", 3);
            Console.WriteLine("({0}) Save and Quit", 4);
        }

        public bool SaveAndQuit ()
        {
            return true;
        }

        public bool ValidateInput(string input)
        {
            return true;
        }
    }
}
