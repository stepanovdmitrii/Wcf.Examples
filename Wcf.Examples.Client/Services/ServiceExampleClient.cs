using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Wcf.Examples.Contracts;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Client.Services
{
    internal sealed class ServiceExampleClient: ClientBase<IServiceExample>, IServiceExample
    {
        public ServiceExampleClient(Binding binding, EndpointAddress endpoint): base(binding, endpoint) { }

        public IAsyncResult BeginLongRunningTask(TaskId taskId, AsyncCallback callback, object state)
        {
            return Channel.BeginLongRunningTask(taskId, callback, state);
        }

        public void CancelTask(TaskId taskId)
        {
            Channel.CancelTask(taskId);
        }

        public void Connect()
        {
            base.Open();
        }

        public string EndLongRunningTask(IAsyncResult result)
        {
            return Channel.EndLongRunningTask(result);
        }

        public string Ping()
        {
            return base.Channel.Ping();
        }
    }
}
