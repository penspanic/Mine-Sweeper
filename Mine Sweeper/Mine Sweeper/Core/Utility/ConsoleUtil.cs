using System;
using System.Collections.Generic;

namespace Mine_Sweeper.Core.Utility
{
    class ConsoleUtil
    {
        public static void Write(string str)
        {
            Console.Write(str);
        }

        public static void Write(string str, Point pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Write(str);
        }

        public static void Write(char ch)
        {
            Console.Write(ch);
        }

        public static void Write(char ch, Point pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Write(ch);
        }
    }
}