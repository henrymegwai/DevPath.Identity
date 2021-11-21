using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.BvnModels
{
    public class BvnResponse
    {
        
        public string BVN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber1 { get; set; }         
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber2 { get; set; }  
        public string MaritalStatus { get; set; } 
        public string ResidentialAddress { get; set; }  
        public string Base64Image { get; set; }
    }
}
