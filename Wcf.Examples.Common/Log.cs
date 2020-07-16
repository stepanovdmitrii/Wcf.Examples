using System;

namespace Wcf.Examples.Common
{
    public static class Log
    {
        private const string InfoFormat = "{0}\tInfo\t{1}";

        public static void Information(string message)
        {
            Console.WriteLine(string.Format(InfoFormat, DateTime.Now, message));
        }

        public static void Information(string message, params object[] args)
        {
            Console.WriteLine(string.Format(InfoFormat, DateTime.Now, string.Format(message, args)));
        }
    }
}
