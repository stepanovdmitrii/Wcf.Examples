using System;
using System.Threading;
using Wcf.Examples.Server.Async;

namespace Wcf.Examples.Server.Service
{
    internal sealed class TaskStub : ITask<string>
    {
        public void Execute(CancellationToken cancellationToken)
        {
            cancellationToken.WaitHandle.WaitOne(TimeSpan.FromMinutes(5));
        }

        public string GetResult()
        {
            return "Task finished";
        }
    }
}
