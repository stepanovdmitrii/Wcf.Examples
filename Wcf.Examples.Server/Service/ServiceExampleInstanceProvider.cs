using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Wcf.Examples.Server.Async;

namespace Wcf.Examples.Server.Service
{
    internal sealed class ServiceExampleInstanceProvider : IInstanceProvider
    {
        private readonly ITaskController _taskController;

        public ServiceExampleInstanceProvider(ITaskController taskController)
        {
            _taskController = taskController;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new ServiceExample(_taskController);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            IDisposable disposable = null;
            try
            {
                disposable = instance as IDisposable;
            }
            finally
            {
                disposable?.Dispose();
            }
        }
    }
}
