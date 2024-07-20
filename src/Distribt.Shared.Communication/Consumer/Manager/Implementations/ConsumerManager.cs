using Distribt.Shared.Communication.Consumer.Manager.Interfaces;

namespace Distribt.Shared.Communication.Consumer.Manager.Implementations
{
    public class ConsumerManager<TMessage>(CancellationTokenSource cancellationTokenSource) : IConsumerManager<TMessage>
    {
        private CancellationTokenSource _cancellationTokenSource = cancellationTokenSource;

        public void RestartExecution()
        {
            var cancellationTokenSource = _cancellationTokenSource;
            _cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
        }

        public void StopExecution() => _cancellationTokenSource.Cancel();

        public CancellationToken GetCancellationToken() => _cancellationTokenSource.Token;
    }
}
