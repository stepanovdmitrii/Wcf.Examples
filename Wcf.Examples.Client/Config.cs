using System;

namespace Wcf.Examples.Client
{
    internal static class Config
    {
        public static int RetryCount { get; } = 5;
        public static TimeSpan RetryTimeOut { get; } = TimeSpan.FromSeconds(5);
        public static TimeSpan TaskPoolTimeout { get; } = TimeSpan.FromSeconds(5);
    }
}
