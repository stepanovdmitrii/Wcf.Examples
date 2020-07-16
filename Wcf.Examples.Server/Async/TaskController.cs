using System;
using System.Collections.Generic;
using System.Threading;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Server.Async
{
    internal interface ITaskController
    {
        AsyncResult<TResult> ExecuteAsync<TResult>(Func<CancellationToken, TResult> func, TaskId taskId, AsyncCallback callback, object state);
        void Cancel(TaskId taskId);
        void Remove(TaskId taskId);
    }

    internal sealed class TaskController: ITaskController, IDisposable
    {
        private readonly object _guard = new object();
        private readonly Dictionary<TaskId, CancellationTokenSource> _cancellations = new Dictionary<TaskId, CancellationTokenSource>();

        public void Cancel(TaskId taskId)
        {
            CancellationTokenSource cancellation = null;
            try
            {
                lock (_guard)
                {
                    if (false == _cancellations.TryGetValue(taskId, out cancellation)) throw new InvalidOperationException("task not found");
                    _cancellations.Remove(taskId);
                }
                cancellation.Cancel();
            }
            finally
            {
                cancellation?.Dispose();
            }
        }

        public void Dispose()
        {
            foreach(var value in _cancellations.Values)
            {
                value.Dispose();
            }
        }

        public AsyncResult<TResult> ExecuteAsync<TResult>(Func<CancellationToken, TResult> func, TaskId taskId, AsyncCallback callback, object state)
        {
            lock (_guard)
            {
                if (_cancellations.ContainsKey(taskId)) throw new InvalidOperationException("taskId must be unique");

                CancellationTokenSource cancellation = null;
                AsyncResult<TResult> result = null;
                try
                {
                    cancellation = new CancellationTokenSource();
                    _cancellations[taskId] = cancellation;
                    cancellation = null;
                    result = new AsyncResult<TResult>(() => func(_cancellations[taskId].Token), taskId, callback, state);
                    result.Start();

                    var tmp = result;
                    result = null;
                    return result;
                }
                finally
                {
                    if(cancellation != null)
                    {
                        _cancellations.Remove(taskId);
                        cancellation.Dispose();
                    }

                    result?.Dispose();
                }
            }
        }

        public void Remove(TaskId taskId)
        {
            lock (_guard)
            {
                CancellationTokenSource cancellation = null;
                try
                {
                    lock (_guard)
                    {
                        if (false == _cancellations.TryGetValue(taskId, out cancellation)) throw new InvalidOperationException("task not found");
                        _cancellations.Remove(taskId);
                    }
                }
                finally
                {
                    cancellation?.Dispose();
                }
            }
        }
    }
}
