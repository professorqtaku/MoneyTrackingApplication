using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    class Menu
    {
        public static void ShowMainMenu(bool clear = false)
        {
            if (clear) Console.Clear();
            Console.WriteLine("Pick an option: ");
            MessageHandler.ShowListNumber(prefix: "1", message: "Show items (All/Expense(s)/Income(s))");
            MessageHandler.ShowListNumber(prefix: "2", message: "Add New Expense/Income");
            MessageHandler.ShowListNumber(prefix: "3", message: "Edit Item (edit, remove)");
            MessageHandler.ShowListNumber(prefix: "4", message: "Save and Quit");
        }

        public static void SaveAndQuit()
        {
            MessageHandler.ShowMessage("Saving data...", ConsoleColor.Yellow);
            // Here you would add code to save your data to a file or database
            MessageHandler.ShowMessage("Data saved successfully. Exiting application.", ConsoleColor.Green);
            Environment.Exit(0);
        }

        public static string GetInput(string? prompt = "", bool allowEmpty = false)
        {
            string input;
            while (true)
            {
                if (!string.IsNullOrEmpty(prompt)) Console.Write(prompt);
                input = Console.ReadLine() ?? string.Empty;
                if (!allowEmpty && string.IsNullOrWhiteSpace(input))
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
            MessageHandler.ShowMessage("Select an option of items to show:");
            MessageHandler.ShowListNumber(prefix: "1", message: "All Items");
            MessageHandler.ShowListNumber(prefix: "2", message: "Expenses");
            MessageHandler.ShowListNumber(prefix: "3", message: "Incomes");
        }
    }
}
