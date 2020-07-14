using Wcf.Examples.Contracts;

namespace Wcf.Examples.Server.Service
{
    internal sealed class ServiceExample : IServiceExample
    {
        public string Ping()
        {
            return "Pong!";
        }
    }
}
