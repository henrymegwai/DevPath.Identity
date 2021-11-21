using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.DomainModels
{
    public class OtpRequestModel : Model
    {
       
        public string PhoneNumber { get; set; }
        
    }

    public class OtpMsgModel : Model
    {
        public string MobileNumber { get; set; }
        public string Body { get; set; }
    }
     

    public class OtpVerifyRequestModel : Model
    {
        public string OtpReceived { get; set; }
        public string PhoneNumber { get; set; }
    }
    
}
