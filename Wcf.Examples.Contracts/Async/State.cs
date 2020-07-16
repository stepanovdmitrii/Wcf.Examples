using System.Runtime.Serialization;

namespace Wcf.Examples.Contracts.Async
{
    [DataContract]
    public enum State
    {
        [EnumMember]
        Created = 0,

        [EnumMember]
        Running = 1,

        [EnumMember]
        Completed = 2
    }
}
