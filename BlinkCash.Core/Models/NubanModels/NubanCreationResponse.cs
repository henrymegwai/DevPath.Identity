using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.NubanModels
{
    public class NubanCreationResponse
    {
        public bool error { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
    }
     
    public class Data
    {
        public int transactionPermission { get; set; }
        public string _id { get; set; }
        public string lastName { get; set; }
        public string otherNamnes { get; set; }
        public string bvn { get; set; }
        public string phoneNumber { get; set; }
        public int gender { get; set; }
        public string placeOfBirth { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string address { get; set; }
        public string accountOfficerCode { get; set; }
        public string emailAddress { get; set; }
        public int notificationPreference { get; set; }
        public string transactionTrackingRef { get; set; }
        public string productCode { get; set; }
        public string merchantId { get; set; }
        public string customerId { get; set; }
        public string accountNumber { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
    }
}
