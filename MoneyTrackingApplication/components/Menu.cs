using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    class Menu
    {
        public static void ShowMainMenu(bool clear = false)
        {
            if (clear) Console.Clear();
            Console.WriteLine("({0}) Show items (All/Expense(s)/Income(s))", 1);
            Console.WriteLine("({0}) Add New Expense/Income", 2);
            Console.WriteLine("({0}) Edit Item (edit, remove)", 3);
            Console.WriteLine("({0}) Save and Quit", 4);
        }

        public static void SaveAndQuit()
        {
            MessageHandler.ShowMessage("Saving data...", ConsoleColor.Yellow);
            // Here you would add code to save your data to a file or database
            MessageHandler.ShowMessage("Data saved successfully. Exiting application.", ConsoleColor.Green);
            Environment.Exit(0);
        }

        public static string GetInput(string prompt = "Enter your choice: ")
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageHandler.ErrorMessage("Input cannot be empty. Please try again.");
                    continue;
                }
                break;
            }

            return input.Trim();
        }

        public static void ShowItemTypeMenu()
        {
            Console.WriteLine("({0}) Show All Items", 1);
            Console.WriteLine("({0}) Show Expenses", 2);
            Console.WriteLine("({0}) Show Incomes", 3);
        }
    }
}
