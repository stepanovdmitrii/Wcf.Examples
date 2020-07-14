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

                    for(int i = 0; i < 5; ++i)
                    {
                        var response = client.Ping();
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        Console.WriteLine($"{DateTime.Now} :  {response}");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
