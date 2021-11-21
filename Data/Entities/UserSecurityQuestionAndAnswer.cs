using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class UserSecurityQuestionAndAnswer : BaseEntity
    { 
        public string UserId { get; set; }
        public string Answer { get; set; }
        public long SecurityQuestionId { get; set; }
        public virtual SecurityQuestion SecurityQuestion { get; set; }
    }
}
