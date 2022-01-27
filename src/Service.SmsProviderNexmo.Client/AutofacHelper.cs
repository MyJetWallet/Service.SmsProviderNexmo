using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;
using Service.SmsProviderNexmo.Domain.Models;
using Service.SmsSender.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.SmsProviderNexmo.Client
{
    public static class AutofacHelper
    {
        public static void RegisterSmsProviderNexmoClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new SmsProviderNexmoClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetSmsDeliveryService()).As<ISmsDeliveryService>().SingleInstance();
        }

        public static void RegisterNexmoReportMessagePublisher(this ContainerBuilder builder,
            MyServiceBusTcpClient client )
        {
            builder.RegisterMyServiceBusPublisher<NexmoSmsDeliveryReportMessage>(client, NexmoSmsDeliveryReportMessage.TopicName, true);
        }
    }
}
