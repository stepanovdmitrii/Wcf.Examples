using System.ServiceModel;
using Wcf.Examples.Contracts;
using Wcf.Examples.Server.Async;

namespace Wcf.Examples.Server.Service
{
    internal static class ServiceFactory
    {
        public static ServiceHost CreateServiceExample(ITaskController taskController)
        {
            ServiceHost<IServiceExample, ServiceExample> host = null;
            try
            {
                var uri = UriFactory.CreateBaseAddress(WcfConfiguration.ServiceExampleName);
                host = new ServiceHost<IServiceExample, ServiceExample>(uri, new ServiceExampleInstanceProvider(taskController));

                var binding = BindingFactory.CreateBinding();
                host.SetBinding(binding);
                

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
