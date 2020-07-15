using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Wcf.Examples.Contracts
{
    public static class BindingFactory
    {
        
        public static Binding CreateBinding()
        {
            var binding = new NetTcpBinding(SecurityMode.None);
            binding.ReliableSession = new OptionalReliableSession
            {
                Enabled = true,
                Ordered = true,
                InactivityTimeout = WcfConfiguration.InactivityTimeout
            };
            binding.ReceiveTimeout = WcfConfiguration.ReceiveTimeout;

            binding.OpenTimeout = WcfConfiguration.OpenTimeout;
            binding.CloseTimeout = WcfConfiguration.CloseTimeout;
            binding.SendTimeout = WcfConfiguration.SendTimeout;

            return binding;
        }
    }
}
