using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Wcf.Examples.Contracts
{
    public static class BindingFactory
    {
        
        public static Binding CreateBinding()
        {
            var binding = new NetTcpBinding(SecurityMode.None);

            return binding;
        }
    }
}
