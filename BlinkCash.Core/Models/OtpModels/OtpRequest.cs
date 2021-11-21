using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.OtpModels
{
    public class OtpRequest
    {
        public string PhoneNumber { get; set; }
    }
    public class VerifyOtpPhoneNumberRequest
    {
        public string PhoneNumber { get; set; }
    }

    public class VerifyOtpPhoneNumberResponse
    {
        public string Otp { get; set; }
    }

}
