namespace Distribt.Shared.Communication.Consumer.Manager.Interfaces
{
    public interface IConsumerManager<TMessage>
    {
        CancellationToken GetCancellationToken();
        void RestartExecution();
        void StopExecution();
    }
}