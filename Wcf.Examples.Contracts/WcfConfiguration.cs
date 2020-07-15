using System;

namespace Wcf.Examples.Contracts
{
    public static class WcfConfiguration
    {
        public const string ServiceExampleName = "ServiceExample";
        public static TimeSpan InactivityTimeout { get; } = TimeSpan.FromSeconds(10);
        public static TimeSpan ReceiveTimeout { get; } = TimeSpan.FromSeconds(30);
        public static TimeSpan OpenTimeout { get; } = TimeSpan.FromSeconds(5);
        public static TimeSpan CloseTimeout { get; } = TimeSpan.FromSeconds(5);
        public static TimeSpan SendTimeout { get; } = TimeSpan.FromSeconds(5);
    }
}
