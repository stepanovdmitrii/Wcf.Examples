using System;
using System.Threading;
using Wcf.Examples.Contracts;
using Wcf.Examples.Contracts.Async;
using Wcf.Examples.Server.Async;

namespace Wcf.Examples.Server.Service
{
    internal sealed class ServiceExample : IServiceExample
    {
        private readonly ITaskController _taskController = new TaskController();

        public IAsyncResult BeginLongRunningTask(TaskId taskId, AsyncCallback callback, object state)
        {
            return _taskController.ExecuteAsync<string>((token) =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return "response";
            }, taskId, callback, state);
        }

        public void CancelTask(TaskId taskId)
        {
            _taskController.Cancel(taskId);
        }

        public string EndLongRunningTask(IAsyncResult result)
        {
            if (result != null)
            {
                var res = result as AsyncResult<string>;
                if (res == null) throw new InvalidOperationException();
                _taskController.Remove(res.TaskId);
                return res.Result;
            }
            throw new ArgumentNullException(nameof(result));
        }

        public string Ping()
        {
            return "Pong!";
        }
    }
}
