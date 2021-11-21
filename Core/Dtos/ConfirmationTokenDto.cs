using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class ConfirmationTokenDto: BaseDto
    {
        public string TokenId { get; set; }
        public string SSOToken { get; set; }
    }
}
