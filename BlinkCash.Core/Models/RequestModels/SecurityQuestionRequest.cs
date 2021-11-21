using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.RequestModels
{
    public class SecurityQuestionRequest : Model
    {
        [Required]
        public string Question { get; set; }
    }
    public class UserSecurityQuestionAndAnswerRequest : Model
    {
        [Required]
        public long SecurityQuestionId { get; set; }
        [Required]
        public string Answer { get; set; }
    }
    public class UserSecurityQuestionAndAnswerResponse
    {
         
        public long SecurityQuestionId { get; set; }         
        public string Answer { get; set; }
        public string SecurityQuestion { get; set; }
    }
    public class VerifyAnswerRequest : Model
    {
        [Required]
        public long SecurityQuestionId { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
    public class VerifyAnswerResponse
    {

        public bool DoesAnswerMatch { get; set; }
        public string Answer { get; set; }
        public string SecurityQuestion { get; internal set; }
    }
}
