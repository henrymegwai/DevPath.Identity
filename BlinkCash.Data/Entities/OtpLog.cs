using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class OtpLog : BaseEntity
    {
        public string Reference { get; set; }
        public string OtpHash { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
    }
}
