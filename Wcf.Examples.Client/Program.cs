using System;
using System.Threading;
using Wcf.Examples.Common;

namespace Wcf.Examples.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var client = ClientFactory.CreateServiceExample())
                {
                    
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Log.Information("Openning client...");
                    client.Open();
                    client.InnerChannel.Faulted += InnerChannel_Faulted;
                    Log.Information("Client opened");
                    client.Ping();
                    Console.ReadKey();
                    Log.Information("Closing client...");
                    client.Close();
                }
                Log.Information("Client closed");
                Log.Information("Completed");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static void InnerChannel_Faulted(object sender, EventArgs e)
        {
            Log.Information("Client faulted");
        }
    }
}
