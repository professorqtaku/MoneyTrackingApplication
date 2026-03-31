using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    internal class ItemManager
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public ItemManager() {
            // create some dummy data for testing
            Items.Add(new Item("Groceries", 150.75f, DateTime.Now.AddDays(-2), ItemType.Expense));
            Items.Add(new Item("Salary", 3000f, DateTime.Now.AddDays(-10), ItemType.Income));
            Items.Add(new Item("Electricity Bill", 60.5f, DateTime.Now.AddDays(-5), ItemType.Expense));
            Items.Add(new Item("Freelance Project", 500f, DateTime.Now.AddDays(-15), ItemType.Income));

        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void EditItem(Item oldItem, Item newItem)
        {
            int index = Items.IndexOf(oldItem);
            if (index != -1)
            {
                Items[index] = newItem;
            }
        }

        public void RemoveItem(Item item) {
            Items.Remove(item);
        }

        public void HandleDisplayItems()
        {
            while (true)
            {
                Console.WriteLine(new string('-', 80));
                Menu.ShowItemTypeMenu();
                MessageHandler.ShowMessage("Select one of the options or enter 'q' to quit.");
                string choice = Menu.GetInput();
                if(choice.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                switch (choice)
                {
                    case "1":
                        DisplayItems();
                        break;
                    case "2":
                        DisplayItems(ItemType.Expense);
                        break;
                    case "3":
                        DisplayItems(ItemType.Income);
                        break;
                    default:
                        MessageHandler.ErrorMessage("Invalid choice. Showing all items.");
                        HandleDisplayItems();
                        break;
                }
                Console.WriteLine();
            }
        }

        public void DisplayItems(ItemType? type = null)
        {
            bool showType = type == null;
            var list = showType ? Items : Items.Where(item => item.Type == type.Value).ToList();
            string tableFormat = showType ? "{0,-30} {1,12} {2,20} {3,10}" : "{0,-30} {1,12} {2,20}";

            // Header
            if (showType)
                Console.WriteLine(tableFormat, "Title", "Amount", "Date", "Type");
            else
                Console.WriteLine(tableFormat, "Title", "Amount", "Date");
            Console.WriteLine(new string('-', 80));

            if (list.Count == 0)
            {
                Console.WriteLine("No items to display.");
                return;
            }

            foreach (var item in list)
            {
                // Format: Title (left, 30), Amount (right, 12) with two decimals + kr, Date ( yyyy-MM-dd ), Type (optional)
                if (showType)
                {
                    Console.WriteLine(tableFormat,
                        item.Title,
                        item.Amount.ToString("0.00") + "kr",
                        item.Date.ToString("yyyy-MM-dd"),
                        item.Type);
                }
                else
                {
                    Console.WriteLine(tableFormat,
                        item.Title,
                        item.Amount.ToString("0.00") + "kr",
                        item.Date.ToString("yyyy-MM-dd"));
                }
            }
        }


        public void HandleAddItem()
        {
            MessageHandler.ShowMessage("Adding a new item. Enter 'q' at any prompt to cancel.");

            var title = PromptTitle();
            if (title == null) return;

            var amount = PromptAmount();
            if (amount == null) return;

            var date = PromptDate();
            if (date == null) return;

            var itemType = PromptType();
            if (itemType == null) return;

            var newItem = new Item(title, amount.Value, date.Value, itemType.Value);
            Items.Add(newItem);
            MessageHandler.ShowMessage("Item added.", ConsoleColor.Green);

        }

        // Prompts for title. Returns null when user cancels.
        private string? PromptTitle()
        {
            string prompt = "Title: ";
            string input = Menu.GetInput(prompt);
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            while (string.IsNullOrWhiteSpace(input))
            {
                MessageHandler.ErrorMessage("Title cannot be empty. Please enter a title or 'q' to cancel.");
                input = Menu.GetInput(prompt);
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;
            }

            return input;
        }

        // Prompts for amount. Returns null when user cancels.
        private float? PromptAmount()
        {
            string prompt = "Amount: ";
            MessageHandler.ShowMessage("Amount (use dot as decimal separator), e.g. 123.45:");
            string input = Menu.GetInput(prompt);
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            if (float.TryParse(input, out var amount)) return amount;

            while (!float.TryParse(input, out amount))
            {
                MessageHandler.ErrorMessage("Invalid amount. Please enter a numeric value or 'q' to cancel.");
                input = Menu.GetInput(prompt);
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;
            }

            return amount;
        }

        // Prompts for date. Returns null when user cancels.
        private DateTime? PromptDate()
        {
            string prompt = "Date: ";
            MessageHandler.ShowMessage("Date (yyyy-MM-dd) or leave empty for today:");
            string input = Menu.GetInput(prompt);
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            if (string.IsNullOrWhiteSpace(input)) return DateTime.Now;

            if (DateTime.TryParse(input, out var date)) return date;

            while (!DateTime.TryParse(input, out date))
            {
                MessageHandler.ErrorMessage("Invalid date. Enter in format yyyy-MM-dd or 'q' to cancel.");
                input = Menu.GetInput(prompt);
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;
            }

            return date;
        }

        // Prompts for type. Returns null when user cancels.
        private ItemType? PromptType()
        {
            MessageHandler.ShowMessage("Type ('expense' or 'income') - leave empty for expense:");
            string input = Menu.GetInput();
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            if (string.IsNullOrWhiteSpace(input)) return ItemType.Expense;

            if (ItemTypeHelper.TryParse(input, out var t)) return t;

            MessageHandler.ErrorMessage("Invalid type provided. Defaulting to 'expense'.");
            return ItemType.Expense;
        }

    }
}
