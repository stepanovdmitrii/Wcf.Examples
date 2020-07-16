using System;
using System.Threading;
using System.Threading.Tasks;
using Wcf.Examples.Contracts.Async;

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
                    var task = Task<string>.Factory.FromAsync((callback, state) => client.BeginLongRunningTask(TaskId.New(), callback, state), client.EndLongRunningTask, client);
                    Console.WriteLine(task.Result);
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
            Console.WriteLine($"{DateTime.Now} : client faulted");
        }
    }
}
