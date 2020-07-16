using System;
using System.ServiceModel;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Contracts
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IServiceExample
    {

        [OperationContract]
        TaskId StartLongRunningTask();

        [OperationContract]
        string GetLongRunningTaskResult(TaskId taskId);

        [OperationContract]
        TaskStatus GetTaskStatus(TaskId taskId);

        [OperationContract]
        void CancelTask(TaskId taskId);
    }
}
