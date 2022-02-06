using RH.Core.Messages.Integrations;
using System;
using System.Threading.Tasks;

namespace RH.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        Task PublishAsync<T>(T message) where T : IntegrationEvent;
        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent where TResponse : ResponseIntegrationMessage;
        IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder) where TRequest : IntegrationEvent where TResponse : ResponseIntegrationMessage;
        bool IsConnected { get; }
    }
}