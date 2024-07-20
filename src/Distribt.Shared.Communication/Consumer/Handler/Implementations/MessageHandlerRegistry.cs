﻿using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using System.Collections.Concurrent;

namespace Distribt.Shared.Communication.Consumer.Handler.Implementations
{
    public class MessageHandlerRegistry(IEnumerable<IMessageHandler> messageHandlers) : IMessageHandlerRegistry
    {
        private readonly IEnumerable<IMessageHandler> _messageHandlers = messageHandlers;

        private readonly ConcurrentDictionary<string, IEnumerable<IMessageHandler>> _cachedHandlers = new();

        public IEnumerable<IMessageHandler> GetMessageHandlerForType(Type messageHandlerType, Type messageType)
        {
            var key = $"{messageHandlerType}-{messageType}";
            if (_cachedHandlers.TryGetValue(key, out var existingHandlers))
            {
                return existingHandlers;
            }

            IList<IMessageHandler> handlers =
                GetMessageHandlersInternal(messageHandlerType, messageType);

            _cachedHandlers.AddOrUpdate(key, handlers.Distinct(), (_, __) => handlers);
            if (handlers.Count == 0)
            {
                // #4 add logging and specify no handlers found.
            }

            return handlers;
        }

        private List<IMessageHandler> GetMessageHandlersInternal(Type messageHandlerType, Type messageType)
        {
            return
                _messageHandlers.Where(
                    h => h.GetType()
                        .GetInterfaces()
                        .Contains(messageHandlerType))
                .Distinct()
                .ToList();
        }
    }
}
