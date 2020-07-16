using System;
using System.Runtime.Serialization;

namespace Wcf.Examples.Contracts.Async
{
    [DataContract]
    public sealed class TaskStatus
    {
        [DataMember]
        public State TaskState { get; set; }

        [DataMember]
        public TimeSpan NextAttemptTimeOut { get; set; }

        [DataMember]
        public string Details { get; set; }
    }
}
