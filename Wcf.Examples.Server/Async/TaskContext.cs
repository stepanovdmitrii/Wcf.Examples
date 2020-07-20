using System;
using System.Threading;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Server.Async
{
    internal interface ITaskContextSafe
    {
        State GetState();
    }

    internal sealed class TaskContext: ITaskContextSafe, IAction, IDisposable
    {
        private readonly ITask _task;
        private readonly TaskId _id;
        private readonly CancellationTokenSource _cancellation;
        private readonly object _guard;
        private readonly IActionExecutor _executor;

        private State _state;
        private Exception _exception;
        
        public TaskContext(TaskId id, ITask task, IActionExecutor executor)
        {
            _task = task;
            _id = id;
            _guard = new object();
            _cancellation = new CancellationTokenSource();
            _state = State.Created;
            _executor = executor;
        }

        public void Dispose()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }

        public void StartExecution()
        {
            lock (_guard)
            {
                _executor.Push(this);
                _state = State.Running;
            }
        }


        public TResult GetResult<TResult>()
        {
            lock (_guard)
            {
                if (_state != State.Completed) throw new InvalidOperationException("Task is not completed");
                if (_exception != null) throw _exception;
                ITask<TResult> result = _task as ITask<TResult>;
                if (result != null) return result.GetResult();
                throw new InvalidOperationException("Invalid task result type");
            }
        }

        public void Cancel()
        {
            _cancellation.Cancel();
        }

        public State GetState()
        {
            lock (_guard)
            {
                return _state;
            }
        }

        public void Do()
        {
            try
            {
                _cancellation.Token.ThrowIfCancellationRequested();
                _task.Execute(_cancellation.Token);
                SetCompleted();
            }
            catch(Exception ex)
            {
                SetException(ex);
            }
        }

        private void SetException(Exception ex)
        {
            lock (_guard)
            {
                _exception = ex;
                _state = State.Completed;
            }
        }

        private void SetCompleted()
        {
            lock (_guard)
            {
                _state = State.Completed;
            }
        }
    }
}
