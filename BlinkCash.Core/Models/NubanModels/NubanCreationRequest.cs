using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.NubanModels
{
    public class NubanCreationRequest
    {
        public string LastName { get; set; }
        public string OtherNamnes { get; set; }
        public string Bvn { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string AccountOfficerCode { get; set; }
        public string EmailAddress { get; set; }
       
    }
}
