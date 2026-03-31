using Money_Tracking_Application.components;
using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application
{
    internal class App
    {
        private ItemManager _itemManager = new ItemManager();
        public void Run()
        {
            while (true)
            {
                Menu.ShowMainMenu();
                var input = Menu.GetInput();
                MessageHandler.ShowMessage($"You entered: {input}", ConsoleColor.Green);
                HandleFirstChoie(input);
                Console.WriteLine();
            }
}

        // This method gets the input from user and handles the choices in a switch statement, it is called in the Run method
        private void HandleFirstChoie(string input)
        {
            switch (input)
            {
                case "1":
                    _itemManager.HandleDisplayItems();
                    break;
                case "2":
                    _itemManager.HandleAddItem();
                    break;
                case "3":
                    //_itemManager.DeleteItem();
                    break;
                case "4":
                    //_itemManager.UpdateItem();
                    break;
                case "5":
                    Menu.SaveAndQuit();
                    break;
                default:
                    MessageHandler.ErrorMessage("Invalid choice. Please try again.");
                    break;
            }
        }

    }
}
