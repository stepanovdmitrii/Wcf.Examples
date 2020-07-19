using System.Runtime.Serialization;

namespace Wcf.Examples.Contracts.Async
{
    [DataContract]
    public sealed class TaskStatus
    {
        [DataMember]
        public State TaskState { get; set; }

        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public int PercentCompleted { get; set; }
    }
}
