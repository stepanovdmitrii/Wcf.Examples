using System;
using System.Collections.Generic;
using System.Threading;

namespace Wcf.Examples.Server.Async
{
    internal sealed class MultiThreadActionExecutor : IActionExecutor, IDisposable
    {
        private const string ThreadTitle = "ActionExecutorThread";
        private readonly Thread[] _threads;
        private readonly object _guard;
        private readonly Queue<IAction> _actions;

        private bool _stopped;

        public MultiThreadActionExecutor(uint threadsCount)
        {
            if (threadsCount == 0) throw new ArgumentException("Threads count must be greater than zero", nameof(threadsCount));
            _stopped = false;
            _guard = new object();
            _actions = new Queue<IAction>();
            _threads = new Thread[threadsCount];
            for(int index = 0; index < _threads.Length; ++index)
            {
                _threads[index] = new Thread(Do);
                _threads[index].Name = ThreadTitle;
                _threads[index].Start();
            }
        }

        public void Dispose()
        {
            lock (_guard)
            {
                _stopped = true;
                Monitor.PulseAll(_guard);
            }

            foreach(var thread in _threads)
            {
                thread.Join();
            }
        }

        public void Push(IAction action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            lock (_guard)
            {
                if (_stopped) throw new InvalidOperationException("Executor is stopped");
                _actions.Enqueue(action);
                Monitor.Pulse(_guard);
            }
        }

        private void Do()
        {
            var action = WaitNext();
            if (action == null) return;
            try
            {
                action.Do();
            }
            catch
            {

            }
        }

        private IAction WaitNext()
        {
            lock (_guard)
            {
                while(_actions.Count == 0 && !_stopped)
                {
                    Monitor.Wait(_guard);
                }

                if (_stopped) return null;

                return _actions.Dequeue();
            }
        }
    }
}
