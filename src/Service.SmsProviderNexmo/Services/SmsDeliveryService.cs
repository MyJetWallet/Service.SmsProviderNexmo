using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.SmsProviderNexmo.Settings;
using Service.SmsSender.Grpc;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;

using SendSmsRequest = Service.SmsSender.Grpc.Models.Requests.SendSmsRequest;
using SendSmsResponse = Service.SmsSender.Grpc.Models.Responses.SendSmsResponse;

namespace Service.SmsProviderNexmo.Services
{
    public class SmsDeliveryService : ISmsDeliveryService
    {
        private readonly ILogger<SmsDeliveryService> _logger;
        private readonly SettingsModel _settingsModel;

        public SmsDeliveryService(ILogger<SmsDeliveryService> logger, SettingsModel settingsModel)
        {
            _logger = logger;
            _settingsModel = settingsModel;
        }

        public Task<SendSmsResponse> SendSmsAsync(SendSmsRequest request)
        {
            var credentials = Credentials.FromApiKeyAndSecret(_settingsModel.NexmoApiKey, _settingsModel.NexmoApiSecret);
            var vonageClient = new VonageClient(credentials);
            var response = vonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest
            {
                To = request.Phone.Replace("+", "")
                    .Replace(" ", "")
                    .Replace("-", "")
                    .Replace("(", "")
                    .Replace(")", ""),
                From = _settingsModel.SenderCompanyName,
                Text = request.Body,
                Type = SmsType.unicode
            });

            if (response.Messages.Any(msg => msg.StatusCode != SmsStatusCode.Success))
            {
                _logger.LogInformation("Sms sending failed.");
                return Task.FromResult(new SendSmsResponse { Status = false, ErrorMessage = "Sms sending failed" });
            }

            _logger.LogInformation("Sms sent successfully");
            return Task.FromResult(new SendSmsResponse { Status = true });
        }
    }
}
