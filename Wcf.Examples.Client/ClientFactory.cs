using System.ServiceModel;
using Wcf.Examples.Client.Services;
using Wcf.Examples.Contracts;

namespace Wcf.Examples.Client
{
    internal static class ClientFactory
    {
        public static ServiceExampleClient CreateServiceExample()
        {
            var binding = BindingFactory.CreateBinding();
            var endPoint = new EndpointAddress(UriFactory.CreateBaseAddress(WcfConfiguration.ServiceExampleName));
            return new ServiceExampleClient(binding, endPoint);
        }
    }
}
