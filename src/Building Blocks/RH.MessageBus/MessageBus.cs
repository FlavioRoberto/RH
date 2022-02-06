using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;
using RH.Core.Messages.Integrations;
using System;
using System.Threading.Tasks;

namespace RH.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;

        public readonly string _connectionString;
        public bool IsConnected => _bus?.IsConnected ?? false;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            return _bus.PublishAsync(message);
        }

        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseIntegrationMessage
        {
            TryConnect();
            return _bus.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseIntegrationMessage
        {
            TryConnect();
            return _bus.RespondAsync(responder);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.SubscribeAsync<T>(subscriptionId, onMessage);
        }

        private void TryConnect()
        {
            if (IsConnected)
                return;

            var policy = Policy.Handle<EasyNetQException>()
                               .Or<BrokerUnreachableException>()
                               .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() => _bus = RabbitHutch.CreateBus(_connectionString));
        }

        public void Dispose()
        {
            _bus.Dispose();
        }

    }
}
