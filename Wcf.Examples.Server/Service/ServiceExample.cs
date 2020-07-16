using System.ServiceModel;
using Wcf.Examples.Contracts;
using Wcf.Examples.Contracts.Async;
using Wcf.Examples.Server.Async;

namespace Wcf.Examples.Server.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal sealed class ServiceExample : IServiceExample
    {
        private readonly ITaskController _taskController;

        public ServiceExample(ITaskController taskController)
        {
            _taskController = taskController;
        }

        public void CancelTask(TaskId taskId)
        {
            return;
        }

        public string GetLongRunningTaskResult(TaskId taskId)
        {
            return "result";
        }

        public TaskStatus GetTaskStatus(TaskId taskId)
        {
            var status = new TaskStatus();
            status.TaskState = State.Completed;
            return status;
        }

        public TaskId StartLongRunningTask()
        {
            return TaskId.New();
        }
    }
}
