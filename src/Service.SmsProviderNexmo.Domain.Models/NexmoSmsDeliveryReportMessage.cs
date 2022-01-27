using System;
using System.Runtime.Serialization;

namespace Service.SmsProviderNexmo.Domain.Models
{
    [DataContract]
    public class NexmoSmsDeliveryReportMessage
    {
        public const string TopicName = "sms-nexmo-report";
        
        [DataMember(Order = 1)] public string PhoneNumber { get; set; }
        [DataMember(Order = 2)] public string NetworkCode { get; set; }
        [DataMember(Order = 3)] public string MessageId { get; set; }
        [DataMember(Order = 4)] public string Price { get; set; }
        [DataMember(Order = 5)] public string Status { get; set; }
        [DataMember(Order = 6)] public string ErrorCode { get; set; }
        [DataMember(Order = 7)] public DateTime MessageTimestamp { get; set; }
    }
}