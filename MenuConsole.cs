using System;

namespace ConsoleSharpProb
{
    //Надоело каждый раз чистить консоль (Этот класс делает это автоматически)
    static class MenuConsole
    {
        public static void WriteLine(string text) =>
            Write(text + '\n');
        public static void Write(string text)
        {
            Console.Clear();
            Console.Write(text);
        }

        public static void WriteLinesAndDescription(string description, string[] text)
        {
            Console.Clear();
            Console.WriteLine(description);
            foreach (string line in text)
                Console.WriteLine(line);
        }
    }
}
