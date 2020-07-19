using System.Threading;

namespace Wcf.Examples.Server.Async
{
    internal interface ITask
    {
        void Execute(CancellationToken cancellationToken);
    }
    internal interface ITask<TResult> : ITask
    {
        TResult GetResult();
    }
}
