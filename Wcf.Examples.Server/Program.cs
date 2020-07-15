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
                Console.WriteLine($"{DateTime.Now} : starting server...");
                using(var host = ServiceFactory.CreateServiceExample())
                {
                    host.Open();

                    Console.WriteLine($"{DateTime.Now} : server started");
                    host.Faulted += Host_Faulted;
                    Console.ReadKey();
                }

                Console.WriteLine($"{DateTime.Now} : stoping server...");
                Console.WriteLine($"{DateTime.Now}: server stoped");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Host_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} : host faulted");
        }
    }
}
