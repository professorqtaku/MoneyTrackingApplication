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
        public static void InfoMessage(string message)
        {
            ShowMessage(message, ConsoleColor.Blue);
        }
        public static void SuccessMessage(string message)
        {
            ShowMessage(message, ConsoleColor.Green);
        }


        public static void ShowListNumber(string message = "", string prefix = "", ConsoleColor messageColor = ConsoleColor.White, ConsoleColor prefixColor = ConsoleColor.DarkMagenta)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            if (!string.IsNullOrEmpty(prefix))
            {
                Console.Write("(");
                Console.ForegroundColor = prefixColor;
                Console.Write(prefix);
                Console.ForegroundColor = previousColor;
                Console.Write(") ");
            }

            if (!string.IsNullOrEmpty(message)) {
                Console.ForegroundColor = messageColor;
                Console.Write(message);
                Console.ForegroundColor = previousColor;
            }
            Console.WriteLine();
        }
        
    }
}
