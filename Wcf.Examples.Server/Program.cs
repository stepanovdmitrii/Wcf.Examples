using System;
using Wcf.Examples.Server.Service;

namespace Wcf.Examples.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting server...");
                using(var host = ServiceFactory.CreateServiceExample())
                {
                    host.Open();

                    Console.WriteLine("Server started");
                    Console.ReadKey();
                }

                Console.WriteLine("Stoping server...");
                Console.WriteLine("Server stoped");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
