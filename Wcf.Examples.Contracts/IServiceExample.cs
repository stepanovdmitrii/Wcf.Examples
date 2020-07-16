using System;
using System.ServiceModel;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Contracts
{
    [ServiceContract]
    public interface IServiceExample
    {
        [OperationContract]
        string Ping();

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginLongRunningTask(TaskId taskId, AsyncCallback callback, object state);

        [OperationContract]
        void CancelTask(TaskId taskId);

        string EndLongRunningTask(IAsyncResult result);
    }
}
