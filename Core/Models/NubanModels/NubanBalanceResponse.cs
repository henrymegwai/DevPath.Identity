using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.NubanModels
{
    public class NubanBalanceResponse
    {
        public bool Error { get; set; }
        public int Data { get; set; }
        public string Message { get; set; }
        public string Nuban { get; set; }
    }
    public class OtpResponse
    {
        public bool Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
         
    }
}
