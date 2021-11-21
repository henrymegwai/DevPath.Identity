using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class IdentityUserExtension : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Address { get; set; }
        public string Device { get; set; }
        public string BVN { get; set; }
        public string PreferredName { get; set; } 
        public bool HasSecretQuestion { get; set; } = false;
        public string PhotoUrl { get; set; } 
        public bool HasResetPasswordForAdmin { get; set; } = false;
        public bool HasPreferredName { get; set; } = false;
        public bool Active { get; set; } = false;
        public string FrequentlyDialedNumbers { get; set; }
    }
}
