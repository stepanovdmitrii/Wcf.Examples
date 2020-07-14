using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Examples.Contracts
{
    public static class UriFactory
    {
        private const string ServiceUriFormat = @"net.tcp://localhost:808/{0}";

        public static Uri CreateBaseAddress(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName)) throw new ArgumentNullException(nameof(serviceName));
            return new Uri(string.Format(ServiceUriFormat, serviceName));
        }
    }
}
