using System;
using System.Threading;

namespace Wcf.Examples.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using(var client = ClientFactory.CreateServiceExample())
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));

                    client.Connect();
                    client.InnerChannel.Faulted += InnerChannel_Faulted;
                    for(int i = 0; i < 5; ++i)
                    {
                        var response = client.Ping();
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        Console.WriteLine($"{DateTime.Now} :  {response}");
                    }
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static void InnerChannel_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} :  faulted");
        }
    }
}
