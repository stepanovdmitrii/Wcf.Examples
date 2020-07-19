using System.ComponentModel;
using Wcf.Examples.Contracts.Async;

namespace Wcf.Examples.Server.Extensions
{
    internal static class StateExtension
    {
        public static Contracts.Async.TaskStatus ToStatus(this State state)
        {
            if(state == State.Created || state == State.Running)
            {
                return new Contracts.Async.TaskStatus()
                {
                    TaskState = state,
                    Details = string.Empty,
                    PercentCompleted = 0
                };
            }
            if(state == State.Completed)
            {
                return new Contracts.Async.TaskStatus()
                {
                    TaskState = state,
                    Details = string.Empty,
                    PercentCompleted = 100
                };
            }

            throw new InvalidEnumArgumentException();
        }
    }
}
