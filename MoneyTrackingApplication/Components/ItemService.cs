using MoneyTrackingApplication.Entities;
using MoneyTrackingApplication.Enums;
using MoneyTrackingApplication.utils;

namespace Money_Tracking_Application.components
{
    internal class ItemService
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public ItemService()
        {
            // create some dummy data for testing
            Items.Add(new Item("Groceries", 150.75f, DateTime.Now.AddDays(-2), ItemType.Expense));
            Items.Add(new Item("Salary", 3000f, DateTime.Now.AddDays(-10), ItemType.Income));
            Items.Add(new Item("Electricity Bill", 60.5f, DateTime.Now.AddDays(-5), ItemType.Expense));
            Items.Add(new Item("Freelance Project", 500f, DateTime.Now.AddDays(-15), ItemType.Income));

        }

        public double GetTotalAmount()
        {
            return Items.Sum(x => x.Amount);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void HandleDisplay()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(new string('-', 80));
                Menu.ShowItemTypeMenu();
                MessageHandler.ShowMessage("Select one of the options or enter 'q' to quit.");
                string choice = Menu.GetInput();
                if (choice.Equals("q", StringComparison.OrdinalIgnoreCase))
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
                        HandleDisplay();
                        break;
                }
                Console.WriteLine();
            }
        }

        public void DisplayItems(ItemType? type = null)
        {
            bool showType = type == null;
            var list = showType
                ? Items.ToList()
                : Items.Where(item => item.Type == type.Value).ToList();

            while (true)
            {
                PrintItems(list, showType);
                var sortOptions = PromptSortOptions();
                if (sortOptions == null)
                { 
                    break;
                }
                else if (sortOptions.HasValue)
                {
                    list = SortItems(list, sortOptions.Value.field, sortOptions.Value.direction);
                }
            }
        }

        private static void PrintItems(List<Item> list, bool showType)
        {
            Console.Clear();
            string tableFormat = showType
                ? "{0,-30} {1,12} {2,20} {3,10}"
                : "{0,-30} {1,12} {2,20}";

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

        private (SortField field, SortDirection direction)? PromptSortOptions()
        {
            MessageHandler.ShowMessage("Sort options: t = Title, a = Amount, d = Date. Press Enter to skip sorting, 'q' to quit.");

            string fieldInput = Menu.GetInput("Sort by: ", allowEmpty: true);

            if(fieldInput.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            SortField field;

            switch (fieldInput.Trim().ToLower())
            {
                case "t":
                    field = SortField.Title;
                    break;

                case "a":
                    field = SortField.Amount;
                    break;

                case "d":
                    field = SortField.Date;
                    break;

                default:
                    field = SortField.Title;
                    break;
            }

            MessageHandler.ShowMessage("Enter order 'asc' or 'desc', leave empty for asc.");
            string dirInput = Menu.GetInput("Order: ", allowEmpty: true);

            SortDirection direction =
                dirInput.Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? SortDirection.Descending
                    : SortDirection.Ascending;

            return (field, direction);
        }

        private static List<Item> SortItems(List<Item> items, SortField field, SortDirection direction)
        {
            Func<Item, object> keySelector = field switch
            {
                SortField.Title => item => item.Title,
                SortField.Amount => item => item.Amount,
                SortField.Date => item => item.Date,
                _ => item => item.Title
            };

            return direction == SortDirection.Ascending
                ? items.OrderBy(keySelector).ToList()
                : items.OrderByDescending(keySelector).ToList();
        }

        public void HandleAdd()
        {
            MessageHandler.ShowMessage("Adding a new item. Enter 'q' at any prompt to cancel.");
            var newItem = PromptItem();
            if (newItem == null) return;
            Items.Add(newItem);
            MessageHandler.ShowMessage("Item added.", ConsoleColor.Green);

        }

        public void HandleSearchAndEdit()
        {
            MessageHandler.ShowMessage("Search for item by title (partial match allowed).");
            var searchWord = Menu.GetInput("Search: ");
            if (searchWord == null)
            {
                MessageHandler.InfoMessage("Search cancelled.");
                return;
            }

            var matchingItems = Items
                .Where(i => i.Title.Contains(searchWord, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchingItems.Count == 0)
            {
                MessageHandler.ErrorMessage($"No items found matching '{searchWord}'.");
                return;
            }

            Item? selectedItem = matchingItems.Count == 1 ? matchingItems[0] : ShowAndSelectItem(matchingItems);

            if (selectedItem != null)
            {
                MessageHandler.SuccessMessage($"Item found: {selectedItem.ToString()}");
                EditItem(selectedItem);
                MessageHandler.ShowMessage("Item updated successfully.", ConsoleColor.Green);
            }
        }

        private static Item? ShowAndSelectItem(List<Item> items)
        {
            MessageHandler.ShowMessage("Select one of the items");
            for (int i = 0; i < items.Count; i++)
            {
                MessageHandler.ShowListNumber(prefix: (i + 1).ToString(), message: items[i].ToString());
            }

            while (true)
            {
                Console.Write("Enter item number (1-" + items.Count + ") or 'q' to cancel: ");
                string input = Menu.GetInput();

                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (int.TryParse(input, out int index) && index > 0 && index <= items.Count)
                {
                    return items[index - 1]; // Convert to 0-based index
                }

                MessageHandler.ErrorMessage($"Invalid input. Please enter a number between 1 and {items.Count}.");
            }
        }

        private static void EditItem(Item item)
        {
            MessageHandler.ShowMessage("Editing item. Leave fields empty to keep current values.");

            var edited = TryCreateItem(allowEmpty: true);
            if (edited == null)
            {
                MessageHandler.InfoMessage("Edit cancelled.");
                return;
            }

            item.SetItem(
                !string.IsNullOrEmpty(edited.Title) ? edited.Title : item.Title,
                edited.Amount != default ? edited.Amount : item.Amount,
                edited.Date != default ? edited.Date : item.Date,
                edited.Type != default ? edited.Type : item.Type
            );
        }

        private static Item? PromptItem(bool tryAgain = true)
        {
            while (true)
            {
                MessageHandler.ShowMessage("Enter the new values.");
                var item = TryCreateItem();

                if (item != null)
                    return item;

                if (!tryAgain)
                    return null;

                Console.WriteLine("Invalid input. Please try again.\n");
            }
        }

        private static Item? TryCreateItem(bool allowEmpty = false)
        {
            var title = PromptTitle(allowEmpty);
            if (!allowEmpty && title == null) return null;

            var amount = PromptAmount(allowEmpty);
            if (!allowEmpty && amount == null) return null;

            var date = PromptDate(allowEmpty);
            if (!allowEmpty && date == null) return null;

            var itemType = PromptType(allowEmpty);
            if (!allowEmpty && itemType == null) return null;

            return new Item(
                title ?? string.Empty,
                amount ?? default,
                date ?? default,
                itemType ?? default
            );
        }


        private static string? PromptTitle(bool allowEmpty = false)
        {
            string prompt = "Title: ";
            string input = Menu.GetInput(prompt, allowEmpty);
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;


            while (string.IsNullOrWhiteSpace(input) && !allowEmpty)
            {
                MessageHandler.ErrorMessage("Title cannot be empty. Please enter a title or 'q' to cancel.");
                input = Menu.GetInput(prompt);
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;
            }

            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        private static double? PromptAmount(bool allowEmpty = false)
        {
            string prompt = "Amount: ";
            MessageHandler.ShowMessage("Amount (use dot as decimal separator), e.g. 123.45:");
            string input = Menu.GetInput(prompt, allowEmpty);
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            if (string.IsNullOrWhiteSpace(input) && allowEmpty)
                return null;

            if (double.TryParse(input, out var amount)) return amount;

            while (!double.TryParse(input, out amount))
            {
                MessageHandler.ErrorMessage("Invalid amount. Please enter a numeric value or 'q' to cancel.");
                input = Menu.GetInput(prompt);
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

                if (string.IsNullOrWhiteSpace(input) && allowEmpty)
                    return null;
            }

            return amount;
        }

        private static DateTime? PromptDate(bool allowEmpty = false)
        {
            string prompt = "Date: ";
            MessageHandler.ShowMessage("Date (yyyy-MM-dd) or leave empty for today:");
            string input = Menu.GetInput(prompt, allowEmpty);
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            if (string.IsNullOrWhiteSpace(input))
                return allowEmpty ? null : DateTime.Now;

            if (DateTime.TryParse(input, out var date)) return date;

            while (!DateTime.TryParse(input, out date))
            {
                MessageHandler.ErrorMessage("Invalid date. Enter in format yyyy-MM-dd or 'q' to cancel.");
                input = Menu.GetInput(prompt);
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

                if (string.IsNullOrWhiteSpace(input))
                    return allowEmpty ? null : DateTime.Now;
            }

            return date;
        }

        private static ItemType? PromptType(bool allowEmpty = false)
        {
            MessageHandler.ShowMessage("Type ('expense' or 'income')");
            string input = Menu.GetInput(allowEmpty: true); // this is for the input itself
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return null;

            if (string.IsNullOrWhiteSpace(input))
                return allowEmpty ? null : ItemType.Expense; // this is in case default value wants to be used

            if (ItemTypeHelper.TryParse(input, out var t)) return t;

            MessageHandler.ErrorMessage("Invalid type provided. Defaulting to 'expense'.");
            return ItemType.Expense;
        }

    }
}
