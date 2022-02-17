using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.SmsProviderNexmo.Domain.Models;
using Service.SmsSender.Domain.Models;
using Service.SmsSender.Domain.Models.Enums;

namespace Service.SmsProviderNexmo.Jobs
{
    public class SmsDeliveryReportJob
    {
        private readonly IServiceBusPublisher<SmsDeliveryMessage> _publisher;
        private readonly ILogger<SmsDeliveryReportJob> _logger;

        public SmsDeliveryReportJob(IServiceBusPublisher<SmsDeliveryMessage> publisher, ISubscriber<NexmoSmsDeliveryReportMessage> subscriber, ILogger<SmsDeliveryReportJob> logger)
        {
            _publisher = publisher;
            _logger = logger;
            subscriber.Subscribe(HandleReports);
        }

        private async ValueTask HandleReports(NexmoSmsDeliveryReportMessage message)
        {
            var status = DeliveryStatus.Accepted;

            switch (message.Status)
            {
                case "accepted":
                    status = DeliveryStatus.Accepted;
                    break;
                case "delivered":
                    status = DeliveryStatus.Delivered;
                    break;
                case "failed":
                case "expired":
                    status = DeliveryStatus.Failed;
                    break;
                default:
                    _logger.LogError("Received unknown status from nexmo. Status: {status}", message.Status);
                    break;
            }

            await _publisher.PublishAsync(new SmsDeliveryMessage
            {
                ExternalMessageId = message.MessageId,
                Status = status,
                ErrorCode = message.ErrorCode
            });
        }
    }
}