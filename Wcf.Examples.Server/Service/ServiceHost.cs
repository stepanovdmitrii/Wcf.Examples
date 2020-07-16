using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Wcf.Examples.Server.Service
{
    internal sealed class ServiceHost<TContract, TImpl>: ServiceHost
    {
        public ServiceHost(Uri uri, IInstanceProvider instanceProvider): base(typeof(TImpl), uri)
        {
            foreach(var contract in ImplementedContracts)
            {
                contract.Value.ContractBehaviors.Add(new InstanceProviderBehaviour(instanceProvider));
            }
        }

        public void SetBinding(Binding binding)
        {
            AddServiceEndpoint(typeof(TContract), binding, string.Empty);
        }
    }
}
