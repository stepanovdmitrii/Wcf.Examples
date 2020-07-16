using System;

namespace Wcf.Examples.Server.Async
{
    internal interface ITaskController
    {

    }

    internal sealed class TaskController : ITaskController, IDisposable
    {
        public void Dispose()
        {
            return;
        }
    }
}
