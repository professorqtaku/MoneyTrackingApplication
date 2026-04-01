using Money_Tracking_Application.components;
using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application
{
    internal class App
    {
        public void Run()
        {
            bool firstRun = true;
            FileHandler.Open();
            WelcomeMessage();
            while (true)
            {
                if(firstRun)
                {
                    firstRun = false;
                }
                else
                {
                    Console.Clear();
                }
                Menu.ShowMainMenu();
                var input = Menu.GetInput();
                MessageHandler.ShowMessage($"You entered: {input}", ConsoleColor.Green);
                HandleFirstChoie(input);
                Console.WriteLine();
            }
        }
        private void WelcomeMessage()
        {
            MessageHandler.ShowMessage("Welcome to TrackMoney");
            double total = ItemService.Instance.GetTotalAmount();
            string presentation = total > 0 ? total.ToString() : "-";
            MessageHandler.ShowMessage($"You have currently ({presentation}) kr on your account.");
        }

        // This method gets the input from user and handles the choices in a switch statement, it is called in the Run method
        private void HandleFirstChoie(string input)
        {
            switch (input)
            {
                case "1":
                    ItemService.Instance.HandleDisplay();
                    break;
                case "2":
                    ItemService.Instance.HandleAdd();
                    break;
                case "3":
                    ItemService.Instance.HandleSearchAndEdit();
                    break;
                case "4":
                    Menu.SaveAndQuit();
                    break;
                default:
                    MessageHandler.ErrorMessage("Invalid choice. Please try again.");
                    break;
            }
        }

    }
}
