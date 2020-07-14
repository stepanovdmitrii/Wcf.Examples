using System;
using System.Threading;

namespace Wcf.Examples.Client.Tools
{
    internal static class Retry
    {
        public static void Do(Action action)
        {
            Exception last = null;
            for(int i = 0; i < Config.RetryCount; ++i)
            {
                try
                {
                    action();
                    return;
                }
                catch(Exception ex)
                {
                    last = ex;
                    Thread.Sleep(Config.RetryCount);
                }
            }
            if(last != null)
            {
                throw new Exception("Retry failed", last);
            }
            throw new Exception("Retry failed");
        }
    }
}
