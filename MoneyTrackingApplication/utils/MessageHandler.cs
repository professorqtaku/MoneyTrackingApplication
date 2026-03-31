using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTrackingApplication.utils
{
    public static class MessageHandler
    {
        // Write a message, optionally changing the console foreground color for the write.
        public static void ShowMessage(string message, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;
                Console.WriteLine(message);
                // restore previous foreground color
                Console.ForegroundColor = previousColor;
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        public static void ErrorMessage(string message)
        {
            ShowMessage(message, ConsoleColor.Red);
        }
    }
}
