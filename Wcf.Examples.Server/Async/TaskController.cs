using System;
using System.Collections.Generic;
using System.Linq;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Server.Async
{
    internal interface ITaskController
    {
        TaskId StartNew(ITask task);
        State GetState(TaskId taskId);
        void CancelTask(TaskId taskId);
        TResult GetResult<TResult>(TaskId taskId);
    }

    internal sealed class TaskController : ITaskController, IDisposable
    {
        private readonly Dictionary<TaskId, TaskContext> _tasks;
        private readonly object _guard;
        private readonly IActionExecutor _actionExecutor;

        public TaskController(IActionExecutor actionExecutor)
        {
            _actionExecutor = actionExecutor;
            _tasks = new Dictionary<TaskId, TaskContext>();
            _guard = new object();
        }


        public void CancelTask(TaskId taskId)
        {
            using(var context = Extract(taskId))
            {
                context.Cancel();
            }
        }

        public void Dispose()
        {
            lock (_guard)
            {
                var ids = _tasks.Keys.ToList();
                foreach(var id in ids)
                {
                    _tasks[id].Dispose();
                }
            }
        }

        public TResult GetResult<TResult>(TaskId taskId)
        {
            using(var context = Extract(taskId))
            {
                return context.GetResult<TResult>();
            }
        }

        public State GetState(TaskId taskId)
        {
            ITaskContextSafe context = null;
            lock (_guard)
            {
                context = GetContext(taskId); 
            }
            return context.GetState();
        }

        public TaskId StartNew(ITask task)
        {
            var id = TaskId.New();
            TaskContext context = null;
            bool inserted = false;
            try
            {
                lock (_guard) {
                    while (_tasks.ContainsKey(id))
                    {
                        id = TaskId.New();
                    }
                    context = new TaskContext(id, task, _actionExecutor);
                    _tasks[id] = context;
                }
                inserted = true;
                context.StartExecution();
                context = null;
                return id;
            }
            catch
            {
                if (inserted)
                {
                    lock (_guard)
                    {
                        _tasks.Remove(id);
                    }
                }
                throw;
            }
            finally
            {
                context?.Dispose();
            }
        }

        private ITaskContextSafe GetContext(TaskId taskId)
        {
            if (false == _tasks.TryGetValue(taskId, out TaskContext context)) throw new InvalidOperationException("Task not found");
            return context;
        }

        private TaskContext Extract(TaskId taskId)
        {
            TaskContext context = null;
            try
            {
                if (false == _tasks.TryGetValue(taskId, out context)) throw new InvalidOperationException("Task not found");
                _tasks.Remove(taskId);
                var tmp = context;
                context = null;
                return tmp;
            }
            finally
            {
                context?.Dispose();
            }

        }
    }
}
