using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using WebApi.Messages;

namespace WebApi.Consumers
{
    public class MyConsumer : IConsumer<MyMessage>
    {
        private readonly ILogger<MyConsumer> _log;

        public MyConsumer(ILogger<MyConsumer> log)
        {
            this._log = log;
        }

        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            var content = context.Message.Content;
            _log.LogInformation($"Time Now: {DateTime.UtcNow}. Time Message: {content}");
            await Task.CompletedTask;
        }
    }
}