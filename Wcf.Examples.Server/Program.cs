﻿using System;
using Wcf.Examples.Server.Async;
using Wcf.Examples.Server.Service;
using Wcf.Examples.Common;
using Wcf.Examples.Server.Configuration;

namespace Wcf.Examples.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Log.Information("Starting server...");
                using(var actionExecutor = new MultiThreadActionExecutor(Config.ThreadsCount))
                using(var taskController = new TaskController(actionExecutor))
                using(var host = ServiceFactory.CreateServiceExample(taskController))
                {
                    host.Open();
                    Log.Information("Server started");
                    host.Faulted += Host_Faulted;
                    Console.ReadKey();
                }

                Log.Information("Server stopped");
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
