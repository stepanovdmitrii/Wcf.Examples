using System.ServiceModel;
using Wcf.Examples.Contracts;

namespace Wcf.Examples.Server.Service
{
    internal static class ServiceFactory
    {
        public static ServiceHost CreateServiceExample()
        {
            ServiceHost host = null;
            try
            {
                var uri = UriFactory.CreateBaseAddress(WcfConfiguration.ServiceExampleName);
                host = new ServiceHost(typeof(ServiceExample), uri);

                var binding = BindingFactory.CreateBinding();
                host.AddServiceEndpoint(typeof(IServiceExample), binding, string.Empty);


                var tmp = host;
                host = null;
                return tmp;
            }
            finally
            {
                if (host != null) host.Close();
            }
        }
    }
}
