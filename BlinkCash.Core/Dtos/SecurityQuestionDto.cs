using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class SecurityQuestionDto :BaseDto
    {
        public string Question { get; set; }
    }
    
    public class UserSecurityQuestionAndAnswerDto : BaseDto
    {
        public string UserId { get; set; }
        public string Answer { get; set; }
        public long SecurityQuestionId { get; set; }
        public virtual SecurityQuestionDto SecurityQuestion { get; set; }
    }
}
