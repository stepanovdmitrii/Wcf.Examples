using System.ServiceModel;
using System.ServiceModel.Channels;
using Wcf.Examples.Contracts;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Client.Services
{
    internal sealed class ServiceExampleClient: ClientBase<IServiceExample>, IServiceExample
    {
        public ServiceExampleClient(Binding binding, EndpointAddress endpoint): base(binding, endpoint) { }

        public void CancelTask(TaskId taskId)
        {
            Channel.CancelTask(taskId);
        }

        public string GetLongRunningTaskResult(TaskId taskId)
        {
            return Channel.GetLongRunningTaskResult(taskId);
        }

        public TaskStatus GetTaskStatus(TaskId taskId)
        {
            return Channel.GetTaskStatus(taskId);
        }

        public void Ping()
        {
            Channel.Ping();
        }

        public TaskId StartLongRunningTask()
        {
            return Channel.StartLongRunningTask();
        }
    }
}
