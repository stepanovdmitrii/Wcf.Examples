using System.ServiceModel;
using Wcf.Examples.Contracts;
using Wcf.Examples.Contracts.Async;
using Wcf.Examples.Server.Async;
using Wcf.Examples.Server.Extensions;

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
            _taskController.CancelTask(taskId);
        }

        public string GetLongRunningTaskResult(TaskId taskId)
        {
            return _taskController.GetResult<string>(taskId);
        }

        public TaskStatus GetTaskStatus(TaskId taskId)
        {
            var state = _taskController.GetState(taskId);
            return state.ToStatus();
        }

        public TaskId StartLongRunningTask()
        {
            return _taskController.StartNew(new TaskStub());
        }
    }
}
