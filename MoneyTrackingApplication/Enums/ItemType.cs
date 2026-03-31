using System;

namespace MoneyTrackingApplication.Enums
{
    public enum ItemType
    {
        Expense,
        Income,
    }

    public static class ItemTypeHelper
    {
        // Try to parse a string into an ItemType. Returns true on success.
        public static bool TryParse(string? input, out ItemType result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(input)) return false;

            var s = input.Trim();

            // Try direct enum parsing (allows values like "INCOME" or "income")
            if (Enum.TryParse<ItemType>(s, true, out var parsed))
            {
                result = parsed;
                return true;
            }

            switch (s.ToLowerInvariant())
            {
                case "expense":
                case "exp":
                case "e":
                case "1":
                    result = ItemType.Expense;
                    return true;
                case "income":
                case "inc":
                case "i":
                case "2":
                    result = ItemType.Income;
                    return true;
                default:
                    return false;
            }
        }

        // Convenience method that returns nullable ItemType (null when parsing fails)
        public static ItemType? GetItemTypeFromString(string? input)
        {
            if (TryParse(input, out var result)) return result;
            return null;
        }
    }
}
