namespace Money_Tracking_Application.components
{
    class Menu
    {
        public void ShowMainMenu()
        {
            Console.WriteLine("({0}) Show items (All/Expense(s)/Income(s))", 1);
            Console.WriteLine("({0}) Add New Expense/Income", 2);
            Console.WriteLine("({0}) Edit Item (edit, remove)", 3);
            Console.WriteLine("({0}) Save and Quit", 4);
        }

        public void HandleMainMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    ShowItemListMenu();
                    break;
                case "2":
                    // Add new item
                    break;
                case "3":
                    // Edit item
                    break;
                case "4":
                    SaveAndQuit();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

        public void ShowItemListMenu()
        {
            Console.WriteLine("({0}) Show all items", 1);
            Console.WriteLine("({0}) Show only expenses", 2);
            Console.WriteLine("({0}) Show only incomes", 3);
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
