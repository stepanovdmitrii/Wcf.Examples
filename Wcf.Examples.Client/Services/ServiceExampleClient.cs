using System.ServiceModel;
using System.ServiceModel.Channels;
using Wcf.Examples.Contracts;

namespace Wcf.Examples.Client.Services
{
    internal sealed class ServiceExampleClient: ClientBase<IServiceExample>, IServiceExample
    {
        public ServiceExampleClient(Binding binding, EndpointAddress endpoint): base(binding, endpoint) { }

        public void Connect()
        {
            base.Open();
        }

        public string Ping()
        {
            return base.Channel.Ping();
        }
    }
}
