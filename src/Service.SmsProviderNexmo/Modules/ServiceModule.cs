using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.SmsProviderNexmo.Domain.Models;
using Service.SmsProviderNexmo.Jobs;
using Service.SmsProviderNexmo.Services;
using Service.SmsSender.Client;

namespace Service.SmsProviderNexmo.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //for debug
            builder
                .RegisterType<SmsDeliveryService>()
                .AutoActivate();
            
            
            var serviceBus = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(t => t.SpotServiceBusHostPort),
                    Program.LogFactory);
            var queue = $"Nexmo-Sms-Provider";
            
            builder.RegisterMyServiceBusSubscriberSingle<NexmoSmsDeliveryReportMessage>(serviceBus,
                NexmoSmsDeliveryReportMessage.TopicName, queue, TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterNexmoReportMessagePublisher(serviceBus);
            
            
            builder
                .RegisterType<SmsDeliveryReportJob>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}