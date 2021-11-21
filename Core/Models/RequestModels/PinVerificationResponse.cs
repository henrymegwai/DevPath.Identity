using System;

namespace BlinkCash.Core.Models.RequestModels
{
    public class PinVerificationResponse
    { 
        public string PhoneNumber { get; set; } 
    }
    public class PinUpdateResponse
    {
        public string PhoneNumber { get; set; }
    }
    //public class OtpResponse
    //{
    //    public DateTime ExpiresOn { get; set; }
    //    public string PhoneNumber { get; set; }
    //}
}
