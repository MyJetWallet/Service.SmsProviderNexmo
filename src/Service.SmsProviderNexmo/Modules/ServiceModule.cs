using Autofac;
using Service.SmsProviderNexmo.Services;

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
        }
    }
}