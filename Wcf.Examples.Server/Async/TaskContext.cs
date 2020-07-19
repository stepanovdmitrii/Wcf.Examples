using System;
using System.Threading;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Server.Async
{
    internal interface ITaskContextSafe
    {
        State GetState();
    }

    internal sealed class TaskContext: ITaskContextSafe, IDisposable
    {
        private readonly ITask _task;
        private readonly TaskId _id;
        private readonly CancellationTokenSource _cancellation;
        private readonly object _guard;

        private State _state;
        
        public TaskContext(TaskId id, ITask task)
        {
            _task = task;
            _id = id;
            _guard = new object();
            _cancellation = new CancellationTokenSource();
            _state = State.Created;
        }

        public void Dispose()
        {

        }

        public void StartExecution()
        {

        }


        public TResult GetResult<TResult>()
        {
            return default(TResult);
        }

        public void Cancel()
        {

        }

        public State GetState()
        {
            return _state;
        }
    }
}
