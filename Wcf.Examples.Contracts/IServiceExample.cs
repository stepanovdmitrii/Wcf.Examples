using System.ServiceModel;

namespace Wcf.Examples.Contracts
{
    [ServiceContract]
    public interface IServiceExample
    {
        [OperationContract]
        string Ping();
    }
}
