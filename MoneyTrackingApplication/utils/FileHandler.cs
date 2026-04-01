using Money_Tracking_Application.components;
using MoneyTrackingApplication.Entities;
using MoneyTrackingApplication.Enums;
using System.Text.Json;

namespace MoneyTrackingApplication.utils
{
    static internal class FileHandler
    {
        static string fileName = string.Format(@"{0}\Item-List.json", Environment.CurrentDirectory);

        public static bool Save()
        {
            //Tries to save the list in a JSON file
            try
            {
                string jsonString = JsonSerializer.Serialize(ItemService.Instance.Items);
                File.WriteAllText(fileName, jsonString);
                MessageHandler.SuccessMessage("Your list has been saved");
                return true;
            }
            catch (Exception)
            {
                MessageHandler.ErrorMessage("Failed to save list!");
                return false;
            }

        }

        public static void Open()
        {
            //If a file exists, load it. Otherwise, create a sample list of tasks.
            if (File.Exists(fileName)) Load();
            else CreateFirstTimeList();
        }
        public static void Load()
        {
            //Try to load a list
            if(!File.Exists(fileName))
            {
                MessageHandler.ErrorMessage("Failed because file doesn't exist");
                return;
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                string jsonString = File.ReadAllText(fileName);
                List<Item>? items = JsonSerializer.Deserialize<List<Item>>(jsonString, options);
                if (items != null)
                {
                    ItemService.Instance.Items = items;
                }
                MessageHandler.SuccessMessage("Loaded your saved list\n");
            }
            catch (Exception)
            {
                MessageHandler.ErrorMessage("Failed to open saved Item list");
            }
        }

        public static void CreateFirstTimeList()
        {
            //Create a populated list of tasks
            ItemService.Instance.Items = [
                new Item("Groceries", 150.75f, DateTime.Now.AddDays(-2), ItemType.Expense),
                new Item("Salary", 3000f, DateTime.Now.AddDays(-10), ItemType.Income),
                new Item("Electricity Bill", 60.5f, DateTime.Now.AddDays(-5), ItemType.Expense),
                new Item("Freelance Project", 500f, DateTime.Now.AddDays(-15), ItemType.Income),
            ];

            MessageHandler.InfoMessage("Did not find a saved list to load. Created a sample list with some incomes/expenses.");
        }
    }
}
