using System;

namespace Wcf.Examples.Server.Configuration
{
    internal static class Config
    {
        public static uint ThreadsCount { get; } = (uint)Environment.ProcessorCount;
    }
}
