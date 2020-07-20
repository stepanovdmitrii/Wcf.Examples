using System;
using System.Threading;
using Wcf.Examples.Common;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var taskId = CreateTask();
                State state = State.Running;
                while (state != State.Completed)
                {
                    Thread.Sleep(Config.TaskPoolTimeout);
                    //CancelTask(taskId);
                    Thread.Sleep(Config.TaskPoolTimeout);
                    state = GetTaskState(taskId);
                }
                string result = GetTaskResult(taskId);
                Log.Information("Completed");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static TaskId CreateTask()
        {
            using (var client = ClientFactory.CreateServiceExample())
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Log.Information("Openning client...");
                client.Open();
                Log.Information("Client opened");
                var taskId = client.StartLongRunningTask();
                Log.Information("Task created: {0}", taskId);
                return taskId;
            }
        }

        private static State GetTaskState(TaskId taskId)
        {
            using (var client = ClientFactory.CreateServiceExample())
            {
                Log.Information("Openning client...");
                client.Open();
                Log.Information("Client opened");
                var state = client.GetTaskStatus(taskId);
                Log.Information("Task state: {0}", state.TaskState);
                return state.TaskState;
            }
        }

        private static void CancelTask(TaskId taskId)
        {
            using (var client = ClientFactory.CreateServiceExample())
            {
                Log.Information("Openning client...");
                client.Open();
                Log.Information("Client opened");
                client.CancelTask(taskId);
                Log.Information("Task cancelled");
            }
        }

        private static string GetTaskResult(TaskId taskId)
        {
            using (var client = ClientFactory.CreateServiceExample())
            {
                Log.Information("Openning client...");
                client.Open();
                Log.Information("Client opened");
                var result = client.GetLongRunningTaskResult(taskId);
                Log.Information("Task result: {0}", result);
                return result;
            }
        }

        private static void InnerChannel_Faulted(object sender, EventArgs e)
        {
            Log.Information("Client faulted");
        }
    }
}
