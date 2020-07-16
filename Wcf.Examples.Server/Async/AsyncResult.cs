using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Server.Async
{
    internal sealed class AsyncResult<TResult> : IAsyncResult, IDisposable
    {
        private readonly ManualResetEvent _manualResetEvent;
        private readonly object _state;
        private readonly AsyncCallback _callback;
        private readonly Func<TResult> _func;

        private TResult _result;
        private Exception _exception;

        public AsyncResult(Func<TResult> func, TaskId taskId, AsyncCallback callback, object asyncState)
        {
            _manualResetEvent = new ManualResetEvent(false);
            _callback = callback;
            _state = asyncState;
            _func = func;
            TaskId = taskId;
        }

        public TResult Result
        {
            get
            {
                if (_exception != null) throw _exception;
                return _result;
            }
        }

        public TaskId TaskId { get; }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute));
        }

        private void Execute(object state)
        {
            try
            {
                _result = _func();
            }
            catch(Exception ex)
            {
                _exception = ex;
            }
            finally
            {
                _manualResetEvent.Set();
                _callback?.Invoke(this);
            }
        }

        public void Dispose()
        {
            _manualResetEvent?.Dispose();
        }

        public bool IsCompleted => _manualResetEvent.WaitOne(0, false);

        public WaitHandle AsyncWaitHandle => null;

        public object AsyncState => _state;

        public bool CompletedSynchronously => false;
    }
}
