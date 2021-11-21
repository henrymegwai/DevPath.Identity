using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class ConfirmationToken : BaseEntity
    {
        public string TokenId { get; set; }
        public string SSOToken { get; set; }
    }
}
