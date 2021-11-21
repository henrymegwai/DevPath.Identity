using BlinkCash.Core.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.DomainModels
{
    public class CheckPhoneNumberExistResponse
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; } 
        public string Device { get; set; } 
        public bool PhoneNumberExist { get; set; } 
        public string PreferredName { get; set; }
        public bool HasTransactionPin { get; set; }
        public bool HasRequestedCard { get; set; }
        public bool HasSecretQuestion { get; set; }
        public string PhotoUrl { get; set; } 
        public bool HasResetPasswordForAdmin { get; set; } 
        public bool HasPreferredName { get; set; }  
        public bool Active { get; set; }
        public AccountModel Account { get; set; }
    }
}
