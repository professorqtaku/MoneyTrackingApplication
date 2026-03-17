using Money_Tracking_Application.components;
using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application
{
    internal class App
    {
        public void Run()
        {
            Console.WriteLine("IN THE APP");
            Menu menu = new Menu();
            while (true)
            {
                menu.ShowMenu();
                var input = RequestChoice();
                if (input == "4")
                {
                    menu.SaveAndQuit();
                    break;
                }
                bool isValid = ValidateInput(input);
                while (!isValid)
                {
                    // try again
                    MessageHandler.ErrorMessage("Invalid input, please try again.");
                    input = RequestChoice();
                    isValid = ValidateInput(input);
                }
                MessageHandler.ShowMessage($"You entered: {input}", ConsoleColor.Green);
            }
}

        private string RequestChoice(string msg = "")
        {
            Console.Write(String.IsNullOrEmpty(msg) ? msg : "Enter your choice: ");
            string input = Console.ReadLine() ?? string.Empty;
            return String.IsNullOrWhiteSpace(input) ? string.Empty : input;
        }

        private bool ValidateInput(string input)
        {
            if (String.IsNullOrEmpty(input)){
                return false;
            }
            return true;
        }

    }
}
