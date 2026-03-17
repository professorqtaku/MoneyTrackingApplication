using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTrackingApplication.utils
{
    public static class MessageHandler
    {
        public static void ShowMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ErrorMessage(string message)
        {
            ShowMessage(message, ConsoleColor.Red);
        }
    }
}
