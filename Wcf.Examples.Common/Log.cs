using System;

namespace Wcf.Examples.Common
{
    public static class Log
    {
        private const string InfoFormat = "{0}\tInfo\t{1}";
        private const string ErrorFormat = "{0}\tError\t{1}";
        private const string ExceptionFormat = "{0}\tException\t{1}\nStackTrace:\n{2}";

        public static void Information(string message)
        {
            Console.WriteLine(string.Format(InfoFormat, DateTime.Now, message));
        }

        public static void Error(string message)
        {
            Console.WriteLine(string.Format(ErrorFormat, DateTime.Now, message));
        }

        public static void Exception(Exception exception)
        {
            Console.WriteLine(string.Format(ExceptionFormat, DateTime.Now, exception.Message, exception.StackTrace));
        }

        public static void Information(string message, params object[] args)
        {
            Console.WriteLine(string.Format(InfoFormat, DateTime.Now, string.Format(message, args)));
        }
    }
}
