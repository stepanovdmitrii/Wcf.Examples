namespace Wcf.Examples.Server.Async
{
    internal interface IActionExecutor
    {
        void Push(IAction action);
    }
}
