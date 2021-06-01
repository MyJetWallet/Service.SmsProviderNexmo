using System;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using Service.SmsProviderNexmo.Client;
using Service.SmsSender.Grpc.Models.Requests;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Console.Write("Press enter to start");
            Console.ReadLine();

            var factory = new SmsProviderNexmoClientFactory("http://localhost:5001");
            var client = factory.GetSmsDeliveryService();

            var resp = await client.SendSmsAsync(new SendSmsRequest
            {
                Phone = "+380973425312",
                Body = "Successful log in from IP 127.0.0.1"
            });

            Console.WriteLine(resp?.Status);

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
