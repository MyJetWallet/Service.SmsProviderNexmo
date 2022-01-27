using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.SmsProviderNexmo.Settings
{
    public class SettingsModel
    {
        [YamlProperty("SmsProviderNexmo.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("SmsProviderNexmo.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("SmsProviderNexmo.NexmoApiKey")]
        public string NexmoApiKey { get; set; }

        [YamlProperty("SmsProviderNexmo.NexmoApiSecret")]
        public string NexmoApiSecret { get; set; }
        
        [YamlProperty("SmsProviderNexmo.SenderCompanyName")]
        public string SenderCompanyName { get; set; }

        [YamlProperty("SmsProviderNexmo.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
        
        [YamlProperty("SmsProviderNexmo.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }
    }
}
