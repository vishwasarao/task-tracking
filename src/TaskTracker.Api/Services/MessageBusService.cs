// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
namespace TaskTracker.Api.Services
{
    public interface IMessageBusService
    {
        void PublishAsync(ServiceBusMessage message);

    }
    public class MessageBusService(ServiceBusClient _client, IOptions<ServiceBusSettings> _settings) : IMessageBusService
    {
        private readonly ServiceBusSender sender = _client.CreateSender(_settings.Value.TopicName);

        public async void PublishAsync(ServiceBusMessage message)
        {
            await sender.SendMessageAsync(message);
        }
    }

    public class ServiceBusSettings()
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string TopicName { get; set; } = string.Empty;
    }
}
