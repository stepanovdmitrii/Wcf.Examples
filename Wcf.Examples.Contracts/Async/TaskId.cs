using System;
using System.Runtime.Serialization;

namespace Wcf.Examples.Contracts.Async
{
    [DataContract]
    public struct TaskId : IEquatable<TaskId>
    {
        [DataMember]
        private Guid _id;

        private TaskId(Guid id)
        {
            _id = id;
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public bool Equals(TaskId other)
        {
            return _id == other._id;
        }

        public static TaskId New() => new TaskId(Guid.NewGuid());
    }
}
