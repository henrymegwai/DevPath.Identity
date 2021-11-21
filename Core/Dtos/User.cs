using BlinkCash.Core.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class User
    {
        public AccountModel Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; }
        public string DateOfBirth { get; set; }
        public string Device { get; set; }
        public string BVN { get; set; }
        public bool HasTransactionPin { get; set; }
        public bool HasSecretQuestion { get; set; }
        public string PhotoUrl { get; set; }
        public string PreferedName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool HasPreferredName { get;   set; }
        public bool HasRequestedCard { get;   set; }
        public string UserName { get;   set; }
        public string Id { get; set; }
    }
}
