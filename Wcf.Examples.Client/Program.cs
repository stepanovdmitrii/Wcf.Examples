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
                using(var client = ClientFactory.CreateServiceExample())
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    Log.Information("Openning client...");
                    client.Open();
                    Log.Information("Client opened");
                    var taskId = client.StartLongRunningTask();
                    Log.Information("Task created: {0}", taskId);
                    client.InnerChannel.Faulted += InnerChannel_Faulted;
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
            Log.Information("Client faulted");
        }
    }
}
